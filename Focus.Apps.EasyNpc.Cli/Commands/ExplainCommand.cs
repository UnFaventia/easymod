using System.ComponentModel;
using Focus.Apps.EasyNpc.Analysis;
using Focus.Apps.EasyNpc.Annotations;
using Focus.Apps.EasyNpc.Data;
using Focus.Providers.Mutagen;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Skyrim;
using Serilog;
using Spectre.Console;
using Spectre.Console.Cli;
using VYaml.Serialization;

namespace Focus.Apps.EasyNpc.Cli.Commands;

using Environment = System.Environment;

public class ExplainCommand(IAnsiConsole console, ILogger logger)
    : AsyncCommand<ExplainCommand.Settings>
{
    public class Settings : CommonSettings
    {
        [CommandArgument(0, "<npc>")]
        [Description(
            "The NPC ID, as either a form key (\"Skyrim.esm:013478\") or Editor ID (\"Delphine\")."
        )]
        public required string NpcId { get; set; }
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        return console
            .Status()
            .AutoRefresh(true)
            .Spinner(Spinner.Known.Dots)
            .StartAsync(
                "Starting up...",
                async ctx =>
                {
                    var profilePath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "EasyNPC",
                        "profiles",
                        "SkyrimSE.yaml"
                    );
                    logger.Debug("Reading profile from {ProfilePath}...", profilePath);
                    using var fs = File.OpenRead(profilePath);
                    var profile = await YamlSerializer.DeserializeAsync<ProfileData>(fs);
                    logger.Information("Read profile from {ProfilePath}.", profilePath);

                    logger.Debug("Loading game data...");
                    ctx.SetIndeterminateStatus("Starting up Mutagen...");
                    var env = GameEnvironmentBuilder<ISkyrimMod, ISkyrimModGetter>
                        .Create(profile.GameName.ToGameRelease())
                        .Build()!;
                    logger.Information("Game data loaded.");
                    ctx.SetIndeterminateStatus("Finding NPC...");
                    var npc = settings.NpcId.Contains(':')
                        ? env.LinkCache.Resolve<INpcGetter>(
                            RecordKey.Parse(settings.NpcId, true).ToFormKey()
                        )
                        : env.LinkCache.Resolve<INpcGetter>(settings.NpcId);
                    if (npc is null)
                    {
                        logger.Error("Couldn't locate NPC with ID {NpcId}.", settings.NpcId);
                        return -1;
                    }
                    logger.Information("Found NPC {NpcId} in load order:", settings.NpcId);
                    AnsiConsole.Write(
                        new Table()
                            .BorderColor(Color.Green)
                            .AddColumn("[bold][aqua]Plugin[/][/]")
                            .AddColumn("[bold][aqua]Form ID[/][/]")
                            .AddColumn("[bold][aqua]Editor ID[/][/]")
                            .AddColumn("[bold][aqua]Name[/][/]")
                            .AddRow(
                                npc.FormKey.ModKey.Name,
                                npc.FormKey.IDString(),
                                npc.EditorID ?? "",
                                npc.Name?.String ?? ""
                            )
                    );

                    logger.Debug("Running analysis...");
                    ctx.SetIndeterminateStatus("Analyzing plugins...");
                    var differ = new SkyrimNpcDiffer();
                    var analyzer = new SessionAnalyzer<ISkyrimModGetter, INpcGetter>(
                        env,
                        differ,
                        profile
                    );
                    using var analyzerProgress = new ProgressAccumulator(analyzer);
                    var analysisTask = analyzer.AnalyzeAsync();
                    while (!analysisTask.IsCompleted)
                    {
                        var progress = analyzerProgress.GetLatest();
                        if (progress is not null)
                        {
                            ctx.SetProgress("Analyzing", progress);
                        }
                        await Task.Delay(TimeSpan.FromMilliseconds(50));
                    }
                    int npcCount = 0; // Temporary until Analysis Result is non-trivial.
                    var finalProgress = analyzerProgress.GetLatest();
                    if (finalProgress is not null)
                    {
                        npcCount = finalProgress.ItemCount;
                    }
                    logger.Information($"Finished analyzing {npcCount} NPCs.", npcCount);
                    ctx.Status = "Finished";
                    var analysis = analysisTask.Result;

                    return 0;
                }
            );
    }
}
