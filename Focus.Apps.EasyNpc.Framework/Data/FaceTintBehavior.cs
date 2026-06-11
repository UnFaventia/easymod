using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Actions that can be taken when an NPC uses non-default face tints (i.e. in the texture set
/// present in the facegen NIF).
/// </summary>
[YamlObject(NamingConvention.UpperCamelCase)]
public enum FaceTintBehavior
{
    /// <summary>
    /// Keep the mod's face tint.
    /// </summary>
    /// <remarks>
    /// Generally used with <see cref="WornArmorBehavior.Clone"/> or
    /// <see cref="WornArmorBehavior.Require"/>.
    /// </remarks>
    Keep,

    /// <summary>
    /// Revert the character's face tint back to vanilla/base defaults.
    /// </summary>
    /// <remarks>
    /// Generally used with <see cref="WornArmorBehavior.Revert"/>.
    /// </remarks>
    Revert,
}
