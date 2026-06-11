using IniParser.Model;

namespace Focus.Apps.EasyNpc.ModManagers.ModOrganizer;

/// <summary>
/// Wrapper for Mod Organizer's root configuration file (<c>ModOrganizer.ini</c>).
/// </summary>
/// <param name="data">Data from the INI file.</param>
/// <param name="instanceDirectoryPath">Location of the instance. Required for when
/// <paramref name="data"/> does not explicitly define a base directory.</param>
internal class ConfigIni(IniData data, string instanceDirectoryPath)
{
    public class GeneralSection(KeyDataCollection data)
    {
        public string? DataPath => AddDataSuffix(GamePath ?? "");
        public string? GameName { get; } = data["gameName"];
        public string? GamePath { get; } = UnescapePath(UnwrapString(data["gamePath"]));
        public string? SelectedProfile { get; } = UnwrapString(data["selected_profile"]);
        public string Version { get; } = data["version"];
    }

    public class SettingsSection
    {
        public string BaseDirectory { get; }
        public string DownloadDirectory { get; }
        public string ModsDirectory { get; }
        public string OverwriteDirectory { get; }
        public string ProfilesDirectory { get; }

        public SettingsSection(KeyDataCollection data, string defaultBaseDirectoryPath)
        {
            BaseDirectory = data["base_directory"] ?? defaultBaseDirectoryPath;
            DownloadDirectory = ResolveDirectory(
                data,
                "download_directory",
                "%BASE_DIR%/downloads",
                BaseDirectory
            );
            ModsDirectory = ResolveDirectory(
                data,
                "mod_directory",
                "%BASE_DIR%/mods",
                BaseDirectory
            );
            OverwriteDirectory = ResolveDirectory(
                data,
                "overwrite_directory",
                "%BASE_DIR%/overwrite",
                BaseDirectory
            );
            ProfilesDirectory = ResolveDirectory(
                data,
                "profiles_directory",
                "%BASE_DIR%/profiles",
                BaseDirectory
            );
        }
    }

    /// <summary>
    /// Loads the metadata from a file.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    public static async Task<ConfigIni> LoadFromFile(string path)
    {
        var data = await Ini.LoadFromFile(path);
        var directory = Path.GetDirectoryName(path);
        return new(data, directory ?? "");
    }

    public GeneralSection General { get; } = new(data["General"] ?? new());

    public SettingsSection Settings { get; } =
        new(data["Settings"] ?? new(), instanceDirectoryPath);

    private static string? AddDataSuffix(string? gamePath)
    {
        if (string.IsNullOrEmpty(gamePath))
        {
            return gamePath;
        }
        var leafDirectory = new DirectoryInfo(gamePath).Name;
        return leafDirectory.Equals("data", StringComparison.OrdinalIgnoreCase)
            ? gamePath
            : Path.Combine(gamePath, "data");
    }

    private static string ResolveDirectory(
        KeyDataCollection data,
        string settingName,
        string defaultValue,
        string baseDirectory
    )
    {
        var value = data[settingName];
        if (string.IsNullOrEmpty(value))
            value = defaultValue ?? string.Empty;
        return value
            .Replace("%BASE_DIR%", baseDirectory, StringComparison.OrdinalIgnoreCase)
            .Replace('/', '\\');
    }

    private static string? UnescapePath(string? maybePath)
    {
        return maybePath?.Replace(@"\\", @"\");
    }

    private static string? UnwrapString(string? maybeWrapped)
    {
        return maybeWrapped?.StartsWith("@ByteArray(") == true
            ? maybeWrapped[11..^1]
            : maybeWrapped;
    }
}
