namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Configuration for a user-defined <see cref="NpcProperty"/> group, as defined by the distinct
/// values in <see cref="ProfileData.PropertyGroups"/>.
/// </summary>
public class PropertyGroupConfiguration
{
    /// <summary>
    /// The priority order of mods participating in this group, in order from lowest (least likely
    /// to be used) to highest (most likely to be used).
    /// </summary>
    public List<ModPriority> ModPriorities { get; set; } = [];
}
