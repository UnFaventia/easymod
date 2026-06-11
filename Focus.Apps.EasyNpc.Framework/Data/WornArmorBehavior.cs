using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Actions that can be taken when an NPC - or an NPC's custom race, depending on the
/// <see cref="CustomRaceBehavior"/> - is using a modded Worn Armor (e.g. custom body or wig).
/// </summary>
[YamlObject(NamingConvention.UpperCamelCase)]
public enum WornArmorBehavior
{
    /// <summary>
    /// Remove any non-vanilla Worn Armor.
    /// </summary>
    /// <remarks>
    /// This was originally used in very old versions of EasyNPC and later replaced by the
    /// <see cref="Clone"/> behavior, but in rare cases may still be a better/preferred choice; it
    /// can prevent certain types of glitches related to e.g. beast transformations, and may help
    /// maintain body consistency if combining with outfit replacers, but may also be visually
    /// inconsistent with face tints resulting in highly visible seams, and is therefore typically
    /// combined with <see cref="FaceTintBehavior.Revert"/>.
    /// </remarks>
    Revert = 0,

    /// <summary>
    /// Copy the worn armor reference exactly, even if it points to the NPC replacer or one of its
    /// dependencies.
    /// </summary>
    /// <remarks>
    /// This will force whichever plugin declares the armor to be a master of the merge.
    /// </remarks>
    Require = 1,

    /// <summary>
    /// Clone the Worn Armor into the merge, attempting to patch compatibility for the NPC's race,
    /// beast transformations, etc.
    /// </summary>
    Clone = 2,
}
