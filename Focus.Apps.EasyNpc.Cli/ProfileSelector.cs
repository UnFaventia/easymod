using Focus.Apps.EasyNpc.Data;
using Serilog;
using Spectre.Console;
using VYaml.Serialization;

namespace Focus.Apps.EasyNpc.Cli;

using Environment = System.Environment;

public class ProfileSelector(
    IAnsiConsole console,
    Environment.SpecialFolder location,
    ILogger logger
)
{
    public ProfileData? ActiveProfile { get; private set; }

    public IReadOnlyList<ProfileData> Profiles => profiles;

    private readonly IAnsiConsole console = console;
    private readonly ILogger logger = logger;
    private readonly string profileDirectory = Path.Combine(
        Environment.GetFolderPath(location),
        "EasyNPC",
        "profiles"
    );

    private readonly List<ProfileData> profiles = [];

    public async Task ScanAsync(string? defaultProfileName = null)
    {
        profiles.Clear();
        logger.Debug("Searching for profiles in {ProfileDirectory}...", profileDirectory);
        var profileFiles = Directory.GetFiles(profileDirectory, "*.yaml");
        logger.Information(
            "Found {ProfileCount} profile(s) in {ProfileDirectory}",
            profileFiles.Length,
            profileDirectory
        );
        var activeProfileName = ActiveProfile?.Name ?? defaultProfileName;
        ActiveProfile = null;
        foreach (var profilePath in profileFiles)
        {
            logger.Debug("Reading profile from {ProfilePath}...", profilePath);
            using var fs = File.OpenRead(profilePath);
            var profile = await YamlSerializer.DeserializeAsync<ProfileData>(fs);
            profile.Name = Path.GetFileNameWithoutExtension(profilePath);
            logger.Information(
                "Loaded profile {ProfileName} from {ProfilePath}.",
                profile.Name,
                profilePath
            );
            profiles.Add(profile);
            if (activeProfileName == profile.Name)
            {
                ActiveProfile = profile;
            }
        }
        if (Profiles.Count == 0)
        {
            logger.Error("No profiles found in {ProfileDirectory}.", profileDirectory);
            return; // TODO: Throw here?
        }
        if (ActiveProfile is null)
        {
            if (Profiles.Count == 1)
            {
                ActiveProfile = Profiles[0];
            }
            else
            {
                ActiveProfile = await new SelectionPrompt<ProfileData>()
                    .Title("Choose game profile")
                    .AddChoices(Profiles)
                    .UseConverter(p => $"{p.Name} ({p.GameName})")
                    .ShowAsync(console, CancellationToken.None);
            }
        }
        logger.Information("Set active profile to {ProfileName}", ActiveProfile.Name);
    }
}
