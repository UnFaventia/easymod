namespace Focus.Apps.EasyNpc.ModManagers.ModOrganizer;

/// <summary>
/// Mod manager implementation based on a Mod Organizer instance.
/// </summary>
public class ModOrganizerModManager : IModManager
{
    public IModRepository ModRepository => modRepository.Value;

    private readonly Lazy<Task<ConfigIni>> configTask;
    private readonly Lazy<IModRepository> modRepository;

    /// <summary>
    /// Creates a new <see cref="ModOrganizerModManager"/> instance.
    /// </summary>
    /// <param name="instanceDirectoryPath">Path to the Mod Organizer instance (i.e. directory
    /// containing <c>ModOrganizer.exe</c>).</param>
    public ModOrganizerModManager(string instanceDirectoryPath)
    {
        configTask = new(
            () => ConfigIni.LoadFromFile(Path.Combine(instanceDirectoryPath, "ModOrganizer.ini")),
            true
        );
        modRepository = new(() => new ModOrganizerModRepository(configTask.Value));
    }

    public async Task<ModManagerConfiguration> GetConfigurationAsync()
    {
        var config = await configTask.Value;
        return new(config.Settings.ModsDirectory, config.General.GamePath, config.General.DataPath);
    }
}
