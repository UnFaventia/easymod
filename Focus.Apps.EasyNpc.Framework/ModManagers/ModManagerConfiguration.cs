namespace Focus.Apps.EasyNpc.ModManagers;

/// <summary>
/// Common configuration settings for all mod managers.
/// </summary>
/// <param name="ModRootDirectory">Directory in which to search for mods/components.</param>
/// <param name="GameExecutableDirectory">Directory containing the game's main executable, if set by
/// the mod manager; otherwise, defaults to the detected install path.</param>
/// <param name="GameDataDirectory">Directory containing the game's data files (e.g. plugins), if
/// set by the mod manager; otherwise, defaults to the <c>Data</c> subdirectory of the game
/// executable directory.</param>
public record ModManagerConfiguration(
    string ModRootDirectory,
    string? GameExecutableDirectory = null,
    string? GameDataDirectory = null
);
