using Newtonsoft.Json;
using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Describes a group that an <see cref="NpcProperty"/> can be assigned to.
/// </summary>
[YamlObject]
public partial class NpcPropertyGroup
{
    /// <summary>
    /// Optional detailed description explaining the meaning or rationale for the group.
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Path to the group's icon to display in various UI.
    /// </summary>
    /// <remarks>
    /// If no icon is specified, or the icon cannot be found, UI will default to displaying the
    /// <see cref="Name"/> as regular text.
    /// </remarks>
    public string IconPath { get; set; } = "";

    /// <summary>
    /// The properties currently assigned to this group.
    /// </summary>
    /// <remarks>
    /// Each property must be assigned to exactly one group, but this is validated at runtime and
    /// the data format uses a nested list instead to bemore human-readable and less error-prone
    /// than separate dictionaries.
    /// </remarks>
    public HashSet<NpcProperty> Properties { get; set; } = [];

    /// <summary>
    /// The priority order of mods participating in this group, in order from lowest (least likely
    /// to be used) to highest (most likely to be used).
    /// </summary>
    public List<ModPriority> ModPriorities { get; set; } = [];
}
