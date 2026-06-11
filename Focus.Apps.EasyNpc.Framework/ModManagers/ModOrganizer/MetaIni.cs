using IniParser.Model;

namespace Focus.Apps.EasyNpc.ModManagers.ModOrganizer;

/// <summary>
/// Wrapper for a Mod Organizer <c>meta.ini</c> file providing mod metadata.
/// </summary>
/// <param name="data">Data from the INI file.</param>
internal class MetaIni(IniData data)
{
    public class GeneralSection(KeyDataCollection data)
    {
        public string? Category { get; } = data["category"];
        public string? Comments { get; } = data["comments"];
        public bool Converted { get; } =
            bool.TryParse(data["converted"], out var converted) && converted;
        public string? GameName { get; } = data["gameName"];
        public string? InstallationFile { get; } = data["installationFile"];
        public string? ModId { get; } = data["modid"];
        public string? NewestVersion { get; } = data["newestVersion"];
        public string? NexusFileStatus { get; } = data["nexusFileStatus"];
        public string? Notes { get; } = data["notes"];
        public string? Repository { get; } = data["repository"];
        public bool Validated { get; } =
            bool.TryParse(data["validated"], out var validated) && validated;
        public string? Version { get; } = data["version"];
    }

    public class InstalledFilesSection(KeyDataCollection data)
    {
        public int Count { get; } = int.TryParse(data["size"], out var count) ? count : -1;
        public string FirstFileId { get; } = data[@"1\fileid"];
        public string FirstModId { get; } = data[@"1\modid"];
    }

    /// <summary>
    /// Loads the metadata from a file.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    public static async Task<MetaIni> LoadFromFile(string path)
    {
        var data = await Ini.LoadFromFile(path);
        return new(data);
    }

    public GeneralSection General { get; } = new(data["General"] ?? new());

    public InstalledFilesSection InstalledFiles { get; } = new(data["installedFiles"] ?? new());
}
