using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Describes how a single mod is prioritized within a property group defined in
/// <see cref="ProfileData.PropertyGroups"/>.
/// </summary>
[YamlObject]
public partial class ModPriority
{
    /// <summary>
    /// Unique <see cref="ModManifest.Key"/> of the mod in this priority slot.
    /// </summary>
    public string ModKey { get; set; } = "";

    /// <summary>
    /// The rules governing when this mod should be excluded from consideration.
    /// </summary>
    public ModExclusionRules Exclusions { get; set; }
}
