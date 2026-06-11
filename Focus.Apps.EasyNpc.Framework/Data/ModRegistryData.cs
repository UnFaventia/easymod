namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Data format for the mod registry, containing information on previously-seen mods along with
/// tracking information and metadata.
/// </summary>
public class ModRegistryData
{
    /// <summary>
    /// The list of known mods.
    /// </summary>
    public List<ModManifest> Mods { get; set; } = [];

    /// <summary>
    /// The "load order" or file priority of mods, from lowest to highest priority.
    /// </summary>
    public List<ModOrderEntry> Order { get; set; } = [];
}

/// <summary>
/// Data for a single entry in the current mod listing.
/// </summary>
public class ModOrderEntry
{
    /// <summary>
    /// The <see cref="ModComponent.Key"/> of the component in this position.
    /// </summary>
    public string ComponentKey { get; set; } = "";

    /// <summary>
    /// Whether or not the referenced component is enabled for this session.
    /// </summary>
    /// <remarks>
    /// Disabled components are present on the file system but deactivated in the mod manager.
    /// Typically, their assets are ignored/blocked, but may be shown with a warning for
    /// informational purposes.
    /// </remarks>
    public bool IsEnabled { get; set; }
}
