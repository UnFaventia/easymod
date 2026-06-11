namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Result of diffing a property (such as <see cref="NpcProperty"/>) against the mod's masters.
/// </summary>
public enum PropertyDiffFlags
{
    /// <summary>
    /// If no other flags are present, indicates that the property value is different from any other
    /// mod in the masters list; i.e. it makes a change that is completely original.
    /// </summary>
    None = 0,

    /// <summary>
    /// The current mod and all its masters all use the same property value; or, the current mod is
    /// the source/first declaration of the related record.
    /// </summary>
    /// <remarks>
    /// Having this flag means that "nothing changed" and that this mod could be removed/ignored
    /// without any effect on the outcome.
    /// </remarks>
    IdenticalToAllMasters = 1,

    /// <summary>
    /// The property value is the same as the one used by the last of this plugin's masters to
    /// modify the same record.
    /// </summary>
    /// <remarks>
    /// If <see cref="IdenticalToAllMasters"/> is not also <c>true</c>, then this indicates a
    /// "carried over" change. Generally, one of two things are true; either (1) the mod is based
    /// on some other mod like USSEP as a foundation (does <b>not</b> also have the
    /// <see cref="IdenticalToFirstMaster"/> or <see cref="IdenticalToMiddleMaster"/> flags), or (2)
    /// the mod is or includes a conflict-resolution patch between two other mods (one of the other
    /// aforementioned flags will be set).
    /// </remarks>
    IdenticalToLastMaster = 2,

    /// <summary>
    /// The property value is the same as the one used by the first master, i.e. the original plugin
    /// that defined the record.
    /// </summary>
    /// <remarks>
    /// Records that are identical to the first master, but not <see cref="IdenticalToAllMasters"/>,
    /// are more likely to be accidental reversions, e.g. due to one of the other masters being
    /// updated and taking on new changes that weren't previously accounted for.
    /// </remarks>
    IdenticalToFirstMaster = 4,

    /// <summary>
    /// The property value is the same as one of the plugin's masters that is neither the first nor
    /// the last to modify the record.
    /// </summary>
    /// <remarks>
    /// If <see cref="IdenticalToAllMasters"/> is not also <c>true</c>, then the meaning of this
    /// flag depends on whether <see cref="IdenticalToLastMaster"/>. If <em>not</em> identical to
    /// the last master then this is reverting the property - possibly unintentionally, but we can't
    /// know for certain. If it <em>is</em> identical to last master (but not all masters, as
    /// originally stated) then this is part of a conflict resolution patch restoring an edit that
    /// one of the previous mods overwrote or reverted.
    /// </remarks>
    IdenticalToMiddleMaster = 8,
}
