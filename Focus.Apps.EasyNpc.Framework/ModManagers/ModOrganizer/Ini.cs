using IniParser;
using IniParser.Model;

namespace Focus.Apps.EasyNpc.ModManagers.ModOrganizer;

/// <summary>
/// Common utilities for working with <c>.ini</c> files.
/// </summary>
internal static class Ini
{
    /// <summary>
    /// Asynchronously loads the data from an INI file on disk.
    /// </summary>
    /// <param name="fileName">Path to the INI file.</param>
    public static async Task<IniData> LoadFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            return new();
        }
        var bytes = await File.ReadAllBytesAsync(fileName);
        using var stream = new MemoryStream(bytes);
        using var reader = new StreamReader(stream);
        return new FileIniDataParser().ReadData(reader);
    }
}
