using System.IO.Abstractions;
using Focus.Environment;
using Mutagen.Bethesda.Plugins.Records;
using Serilog;

namespace Focus.Providers.Mutagen;

public class GameSetup : IGameSetup
{
    public IReadOnlyList<PluginInfo> AvailablePlugins { get; private set; } =
        new List<PluginInfo>().AsReadOnly();
    public string DataDirectory => game.DataDirectory;
    public bool IsConfirmed { get; private set; }
    public ILoadOrderGraph LoadOrderGraph { get; private set; } = new NullLoadOrderGraph();

    private readonly IFileSystem fs;
    private readonly GameInstance game;
    private readonly ILogger log;
    private readonly ISetupStatics setupStatics;

    public GameSetup(IFileSystem fs, ISetupStatics setupStatics, GameInstance game, ILogger log)
    {
        this.fs = fs;
        this.game = game;
        this.log = log;
        this.setupStatics = setupStatics;
    }

    public void Confirm()
    {
        IsConfirmed = true;
    }

    public void Detect(IReadOnlySet<string> blacklistedPluginNames)
    {
        log.Information("Using game data directory: {dataDirectory}", DataDirectory);
        var implicits = setupStatics
            .GetBaseMasters(game.GameRelease)
            .Select(x => x.FileName.String)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
        var listings = setupStatics
            .GetLoadOrderListings(game.GameRelease, DataDirectory, true)
            .ToList();
        var masterResults = listings
            .Select(x => TryGetMasterNames(DataDirectory, x.ModKey.FileName))
            .ToArray();
        AvailablePlugins = listings
            .Zip(masterResults)
            .Select(
                (x, i) =>
                {
                    return new PluginInfo
                    {
                        FileName = x.First.ModKey.FileName.String,
                        IsEnabled = x.First.Enabled,
                        IsImplicit = implicits.Contains(x.First.ModKey.FileName.String),
                        IsReadable = x.Second.Item1,
                        Masters = x.Second.Item2.ToList().AsReadOnly(),
                    };
                }
            )
            .Where(x => x is not null)
            .ToList()
            .AsReadOnly();
        LoadOrderGraph = new LoadOrderGraph(AvailablePlugins, blacklistedPluginNames);
    }

    private (bool, IEnumerable<string>) TryGetMasterNames(
        string dataDirectory,
        string pluginFileName
    )
    {
        var path = fs.Path.Combine(dataDirectory, pluginFileName);
        try
        {
            using var mod = ModInstantiator.ImportGetter(path, game.GameRelease);
            var masterNames = mod.MasterReferences.Select(x => x.Master.FileName.String);
            return (true, masterNames);
        }
        catch (Exception ex)
        {
            log.Warning(
                ex,
                "Plugin {pluginName} appears to be corrupt and cannot be loaded.",
                pluginFileName
            );
            return (false, Enumerable.Empty<string>());
        }
    }
}
