using Focus.Apps.EasyNpc.Annotations;
using Focus.Apps.EasyNpc.IO;
using Focus.Apps.EasyNpc.ModManagers;
using Focus.Apps.EasyNpc.ModManagers.ModOrganizer;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Newtonsoft.Json;
using Noggog;
using Serilog;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Json;

namespace Focus.Apps.EasyNpc.Cli.Commands;

public class ScanCommand(
    IAnsiConsole console,
    IModManagerFactory modManagerFactory,
    ProfileSelector profileSelector,
    ILogger logger
) : AsyncCommand<ScanCommand.Settings>
{
    public class Settings : CommonSettings { }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        await profileSelector.ScanAsync(settings.ProfileName);
        var profile = profileSelector.ActiveProfile;
        if (profile is null)
        {
            logger.Fatal("No profile selected; exiting.");
            return -1;
        }
        return await console
            .Status()
            .AutoRefresh(true)
            .Spinner(Spinner.Known.Dots)
            .StartAsync(
                "Scanning mods...",
                async ctx =>
                {
                    logger.Information(
                        "Starting search of mod directory: {ModDirectory}",
                        profile.ModDirectoryPath
                    );
                    var modManager = modManagerFactory.CreateModManager(profile);
                    var registryData = await modManager.ModRepository.GetModRegistryAsync();
                    logger.Information("Mod indexing completed.");
                    var json = JsonConvert.SerializeObject(registryData);
                    console.Write(
                        new Panel(new JsonText(json)).Header("Available Mods").Collapse()
                    );

                    logger.Debug("Loading game data...");
                    ctx.SetIndeterminateStatus("Starting up Mutagen...");
                    var modManagerConfig = await modManager.GetConfigurationAsync();
                    var gameDirectoryPath = !string.IsNullOrEmpty(profile.GameDirectoryPath)
                        ? profile.GameDirectoryPath
                        : modManagerConfig.GameExecutableDirectory ?? "";
                    var env = GameEnvironmentBuilder<ISkyrimMod, ISkyrimModGetter>
                        .Create(profile.GameName.ToGameRelease())
                        .WithTargetDataFolder(
                            modManagerConfig.GameDataDirectory
                                ?? Path.Combine(gameDirectoryPath, "Data")
                        )
                        .Build()!;
                    logger.Information("Game data loaded.");

                    ctx.SetIndeterminateStatus("Resolving plugin sources");
                    var pluginFileNames = env.LoadOrder.ListedOrder.Select(listing =>
                        listing.FileName
                    );
                    var mapping = ModMapping.Build(
                        registryData,
                        modManagerConfig.ModRootDirectory,
                        pluginFileNames,
                        fileName => Path.Combine(env.DataFolderPath, fileName)
                    );
                    var implicits = Implicits.Get(profile.GameName.ToGameRelease());
                    foreach (var listing in env.LoadOrder.ListedOrder)
                    {
                        if (mapping.TryGetMod(listing.FileName, out var mod))
                        {
                            console.MarkupLine($"{listing.ModKey} is in mod {mod.Name}");
                        }
                        else if (implicits.Listings.Contains(listing.ModKey))
                        {
                            console.MarkupLine($"{listing.ModKey} is an implicit master.");
                        }
                        else
                        {
                            console.MarkupLine(
                                $"[red]Couldn't determine mod for {listing.FileName}.[/]"
                            );
                        }
                    }

                    return 0;
                }
            );
    }
}
