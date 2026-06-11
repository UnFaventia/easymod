using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Actions that can be taken when an NPC is using a custom (mod-added) race.
/// </summary>
[YamlObject(NamingConvention.UpperCamelCase)]
public enum CustomRaceBehavior
{
    /// <summary>
    /// Revert the custom race so that the NPC uses the race from the base game/declaring mod.
    /// </summary>
    /// <remarks>
    /// This is the behavior consistent with versions 0.9.7 and earlier. It is generally safe
    /// because the entire (inherited + overridden) set of head parts will be added to the NPC on
    /// merge, but is incompatible with some mods like Project ja-Kha'jay and anything based on
    /// Charmers of the Reach (CotR).
    /// </remarks>
    Revert,

    /// <summary>
    /// Copy the race reference exactly, even if it points to the NPC replacer or one of its
    /// dependencies.
    /// </summary>
    /// <remarks>
    /// This will force whichever plugin declares the custom race to be a master of the merge.
    /// </remarks>
    Require,

    /// <summary>
    /// Create a patched version of the vanilla/base race that uses the modified head parts and tint
    /// layers.
    /// </summary>
    /// <remarks>
    /// This is a good middle-of-the-road approach that should be compatible with a wide variety of
    /// mods; it avoids creating a master dependency, and won't carry over any behavior changes, but
    /// prevents some difficult-to-resolve conflicts such as the vanilla race having <em>too
    /// many</em> head parts which can't be "removed" at the NPC level.
    /// </remarks>
    Patch,
}
