using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Actions that can be taken when an NPC - or an NPC's custom race, depending on the
/// <see cref="CustomRaceBehavior"/> - is using a modded Worn Armor (e.g. custom body or wig).
/// </summary>
[YamlObject(NamingConvention.UpperCamelCase)]
public enum CustomOutfitBehavior
{
    /// <summary>
    /// Use a behavior consistent with the NPC's <see cref="WornArmorBehavior"/>.
    /// </summary>
    /// <remarks>
    /// Many overhaul-type mods change both the NPC bodies/skins and their outfits, so it is usually
    /// correct to use the same behavior for both in order to ensure that the use a mutually
    /// consistent body mesh. This setting will always follow the NPC's worn-armor setting even if
    /// the specific NPC does not have a worn armor.
    /// </remarks>
    SameAsWornArmor = 3,

    /// <summary>
    /// Remove any non-vanilla Default/Sleep Outfit.
    /// </summary>
    /// <remarks>
    /// This will revert to the game defaults for the NPC's class. May not be compatible if the NPC
    /// has switched to a custom body type with a different model.
    /// </remarks>
    Revert = 0,

    /// <summary>
    /// Copy the armor reference exactly, even if it points to the NPC replacer or one of its
    /// dependencies.
    /// </summary>
    /// <remarks>
    /// This will force whichever plugin declares the armor to be a master of the merge.
    /// </remarks>
    Require = 1,

    /// <summary>
    /// Clone the armor into the merge, attempting to patch compatibility for the NPC's race and any
    /// other attributes.
    /// </summary>
    Clone = 2,
}
