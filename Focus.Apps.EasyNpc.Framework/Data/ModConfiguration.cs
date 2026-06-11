using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Configures the treatment of a source mod during patching/merging.
/// </summary>
[YamlObject]
public partial class ModConfiguration
{
    /// <summary>
    /// Configures overrides for individual field/subrecord behaviors.
    /// </summary>
    [YamlMember("behavior")]
    public BehaviorOverrides BehaviorOverrides { get; set; } = new();
}
