namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Basic information about a plugin, generally within the context of a mod/component.
/// </summary>
public class PluginManifest
{
    /// <summary>
    /// The plugin's name, i.e. its file name including extension, such as "Skyrim.esm".
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Checksum or hash of the plugin's contents. Used for cache invalidation.
    /// </summary>
    public int Hash { get; set; }
}
