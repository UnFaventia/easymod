namespace Focus.Apps.EasyNpc.ModManagers;

/// <summary>
/// Provides access to information about the detected/configured mod manager.
/// </summary>
public interface IModManager
{
    /// <summary>
    /// Gets the mod repository for retrieving information about individual mods.
    /// </summary>
    IModRepository ModRepository { get; }

    /// <summary>
    /// Gets the current configuration parameters for the mod manager.
    /// </summary>
    Task<ModManagerConfiguration> GetConfigurationAsync();
}
