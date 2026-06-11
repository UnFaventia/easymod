namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// A rule for a <see cref="ModPriority"/> that determines whether it should be active for a
/// specific record, or skipped in favor of a lower-priority mod.
/// </summary>
[Flags]
public enum ModExclusionRules
{
    /// <summary>
    /// No exclusions, i.e. always use the values from this plugin and property group, assuming no
    /// other exclusion flags are specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Exclude when the values in this property group are the same as those from the original
    /// plugin that declared the record (e.g. "vanilla").
    /// </summary>
    /// <remarks>
    /// This flag is a broad rule that does not care what other masters modify the properties, and
    /// therefore covers all the cases that <see cref="SameAsAllMasters"/> and
    /// <see cref="ReversionToOriginal"/> would cover, but is not a strict superset of those flags.
    /// </remarks>
    SameAsOriginal = 1,

    /// <summary>
    /// Exclude when the values in this property group are the same as those from any of the
    /// plugin's masters.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This setting can be useful when a plugin has relatively few dependencies of its own but is
    /// based on some framework mod such as USSEP that makes a lot of edits; it will cause any
    /// plugin to be ignored unless its combination of values for this group is <em>unique</em>,
    /// which is typically an intended outcome, i.e. don't overwrite a custom outfit from a fashion
    /// mod (which is not a master) with some trivial tweak made by a framework (which is a master).
    /// </para>
    /// <para>
    /// Results may be less reliable with major "game overhaul" mods requiring many masters, or with
    /// complicated compatibility patches that intentionally revert some edits. Groups that are
    /// insufficiently granular (i.e. a generic "everything but the face" group) also typically
    /// won't benefit much from this flag as they will rarely meet the required condition of having
    /// <b>all</b> properties in the group be identical.
    /// </para>
    /// </remarks>
    SameAsAnyMaster = 2,

    /// <summary>
    /// Exclude when there are no changes to this group anywhere between the original mod, this mod,
    /// and anything in between.
    /// </summary>
    /// <remarks>
    /// This is the "true ITM" flag but on the level of a property group rather than entire records.
    /// It is almost always safe to use this exclusion and expresses the intent of "if a
    /// lower-priority mod (not a master of this one) actually changes something, use it instead."
    /// Best for when overlap of edits is not expected, e.g. when one mod changes outfits but not
    /// faces and another mod changes faces but not outfits.
    /// </remarks>
    SameAsAllMasters = 4,

    /// <summary>
    /// Exclude when the mod reverts ALL edits made by any of its own masters, and reverts the
    /// properties back to their original values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is an advanced flag that may be used to handle incomplete/incorrect patches, version
    /// conflicts and other unusual scenarios, in which it is believed that the mod makes
    /// <em>unintentional</em> edits.
    /// </para>
    /// <para>For example, suppose an NPC's original weight is 1.0, and a master like USSEP changes
    /// it to 0.5, which is then reverted back to 1.0. We can't know from the plugin data alone
    /// whether this is accidental (i.e. USSEP added that edit in a recent version and the mod being
    /// considered is based on an older version) or intentional (the weight actually has to be
    /// changed back to 0.5 because the mod uses a custom outfit that doesn't have a _0 mesh).
    /// Therefore this has to be configurable per mod.
    /// </para>
    /// <para>
    /// Even though reversion to original sounds like a mistake, note that this is in the context of
    /// a plugin's own declared masters and not the entire load order, so this option is actually
    /// not recommended for the majority of cases; only for mods that are either confirmed to be
    /// buggy or have simply not been updated and therefore are out-of-date relative to masters.
    /// </para>
    /// </remarks>
    ReversionToOriginal = 8,

    /// <summary>
    /// Exclude when the mod reverts ANY edit made by a master, and reverts the properties back to
    /// any set of values in any previous master.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is similar to <see cref="ReversionToOriginal"/> but looser, in that it matches property
    /// values identical to any master. For example, USSEP changes a combat style, then some later
    /// combat overhaul mod changes it again, and the mod being considered (which has both USSEP and
    /// the combat overhaul as masters) reverts it back to the USSEP value.
    /// </para>
    /// <para>
    /// Like <see cref="ReversionToOriginal"/>, this is an advanced setting that should normally
    /// only be used when absolutely certain that the author did not intend to make any of those
    /// kinds of changes and that they can only be the result of version conflicts or other errors.
    /// </para>
    /// </remarks>
    ReversionToMaster = 16,
}
