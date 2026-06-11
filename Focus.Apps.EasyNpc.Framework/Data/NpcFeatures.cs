namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Describes a feature of a modded NPC.
/// </summary>
/// <remarks>
/// "Feature" here is used in the context of e.g. a "map feature", as opposed to an "app feature".
/// It simply refers to something that is notable and present in the plugin/facegen, not necessarily
/// a thing that provides value.
/// </remarks>
[Flags]
public enum NpcFeatures
{
    /// <summary>
    /// Placeholder flag indicating no other features.
    /// </summary>
    None = 0,

    /// <summary>
    /// The NPC has a <see cref="ReassignedHeadPart"/> and the new head part was added by a mod.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Custom head parts must always be cloned into the merge in order to be consistent with the
    /// facegen files and avoid blackface, crashing, etc.
    /// </para>
    /// <para>
    /// If the NPC itself was added by a mod, then this flag is only set if the head part was added
    /// by a different mod.
    /// </para>
    /// </remarks>
    CustomHeadPart = 1,

    /// <summary>
    /// The NPC has a <see cref="ReassignedOutfit"/> and the new outfit was added by a mod.
    /// </summary>
    /// <remarks>
    /// If the NPC itself was added by a mod, then this flag is only set if the outfit was added by
    /// a different mod.
    /// </remarks>
    CustomOutfit = 2,

    /// <summary>
    /// If the NPC has a <see cref="CustomOutfit"/> then this indicates that at least one "piece" of
    /// the outfit, i.e. one of its armor references, was also added by a mod.
    /// </summary>
    /// <remarks>
    /// Custom outfits that simply mix existing/vanilla pieces in a new way don't require a lot of
    /// special treatment; the form list itself can be cloned. However, once there are brand-new
    /// armors then the <see cref="CustomOutfitBehavior"/> comes into play.
    /// </remarks>
    CustomOutfitPiece = 4,

    /// <summary>
    /// The NPC has a <see cref="ReassignedRace"/> and the new race was added by a mod.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When encountered in a merge, this will invoke the <see cref="CustomRaceBehavior"/>
    /// configured for the mod.
    /// </para>
    /// <para>
    /// If the NPC itself was added by a mod, then this flag is only set if the race was added by a
    /// different mod.
    /// </para>
    /// </remarks>
    CustomRace = 8,

    /// <summary>
    /// The NPC has a <see cref="ReassignedWornArmor"/> and the new armor was added by a mod.
    /// </summary>
    /// <remarks>
    /// If the NPC itself was added by a mod, then this flag is only set if the armor was added by a
    /// different mod.
    /// </remarks>
    CustomWornArmor = 16,

    /// <summary>
    /// The NPC's race is changed from the plugin's last master containing the same NPC.
    /// </summary>
    /// <remarks>
    /// Does not necessarily imply <see cref="CustomRace"/>, i.e. it could be reverting a custom
    /// race back to a vanilla race. However, any change to the race typically requires a new
    /// facegen unless the substituted race has identical head parts and tints.
    /// </remarks>
    ReassignedRace = 32,

    /// <summary>
    /// The NPC's head parts are changed from the plugin's last master containing the same NPC.
    /// </summary>
    /// <remarks>
    /// Changing head parts almost always requires a new facegen, except in a few rare scenarios
    /// such as when a newly added head part has no model.
    /// </remarks>
    ReassignedHeadPart = 64,

    /// <summary>
    /// The NPC's default outfit is changed from the plugin's last master containing the same NPC.
    /// </summary>
    /// <remarks>
    /// Special treatment is only warranted if the body model has also been changed.
    /// </remarks>
    ReassignedOutfit = 128,

    /// <summary>
    /// The NPC's worn armor (body/skin/wig/etc.) is changed from the plugin's last master
    /// containing the same NPC.
    /// </summary>
    /// <remarks>
    /// Does not generally require special patching, unless it is also a
    /// <see cref="CustomWornArmor"/>. However, if the change to the Worn Armor changes the actual
    /// body <em>model</em> used by the NPC (e.g. from vanilla to CBBE, or CBBE to UNP, or even
    /// removing any of the above and reverting to vanilla), then this can cause issues if other
    /// features such as <see cref="ReassignedOutfit"/> are <em>not</em> carried over, as different
    /// skins have different meshes and may not have correct proportions or textures/tints.
    /// </remarks>
    ReassignedWornArmor = 256,
}
