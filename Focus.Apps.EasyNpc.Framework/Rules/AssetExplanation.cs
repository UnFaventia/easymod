namespace Focus.Apps.EasyNpc.Rules;

/// <summary>
/// Provides info on a specific asset chosen for merge, and the reasons for choosing it.
/// </summary>
public class AssetExplanation
{
    /// <summary>
    /// Whether or not the asset is actually available for merging.
    /// </summary>
    /// <remarks>
    /// In most cases, this will be <c>true</c> if <see cref="Path"/> is non-empty and <c>false</c>
    /// otherwise, but specific combinations (e.g. <see cref="AssetReason.Unavailable"/> for a
    /// pinned asset, or <see cref="AssetReason.Unused"/> being valid) are also covered.
    /// </remarks>
    public bool IsValid =>
        Reason == AssetReason.Unused
        || (!string.IsNullOrWhiteSpace(Path) && Reason != AssetReason.Unavailable);

    /// <summary>
    /// The path to the asset, relative to the mod root directory, and including the mod/component
    /// directory name.
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    /// The reason for choosing the given <see cref="Path"/>.
    /// </summary>
    public AssetReason Reason { get; set; }
}

/// <summary>
/// Explains the reason why a specific asset was chosen.
/// </summary>
public enum AssetReason
{
    /// <summary>
    /// The asset could not be found in any clearly-related location.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This may mean the asset is missing entirely, or could be due to situations like
    /// poorly-designed "merge plugin" AIOs that depend on multiple other mods being active for
    /// their assets but not having any of their plugins active or any other way to trace the
    /// dependency between those mods.
    /// </para>
    /// <para>
    /// If this reason is specified, then <see cref="AssetExplanation.Path"/> will normally be
    /// empty. If a non-empty path is present, then this usually means that the asset was intended
    /// to be <see cref="Pinned"/> but the target no longer exists (was uninstalled or disabled).
    /// </para>
    /// </remarks>
    Unavailable,

    /// <summary>
    /// The relevant asset was found in the specific mod requiring it.
    /// </summary>
    /// <remarks>
    /// This is the normal "everything OK" result to expect in most cases; whichever mod last edited
    /// the record data to require some asset, also has that asset.
    /// </remarks>
    FoundInTarget,

    /// <summary>
    /// The relevant asset was not found in the specific mod requiring it, but was found in one of
    /// the mod's masters; the <see cref="AssetExplanation.Path"/> will indicate which one.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This usually means that a conflict-resolution patch matched one of the priorities, most
    /// often the result of users blindly installing every patch they see in pre-1.0 versions, but
    /// in 1.0+ could be the legitimate result of handcrafted compatibility/CR patches made for a
    /// mod list and intentionally incorporated into the profile.
    /// </para>
    /// <para>
    /// Since mods do not have masters, only plugins do, this actually means that the asset was
    /// found in some mod <em>containing one of the master plugins</em>, which is an approximation
    /// but usually an accurate one. If the same plugin exists in multiple active mods, and at least
    /// two of them have their own copy of the asset, then the result may be ambiguous and
    /// inconsistent across runs.
    /// </para>
    /// </remarks>
    FoundInMaster,

    /// <summary>
    /// The asset was chosen because it was pinned to a specific mod/component, and not affected by
    /// load order or mod priorities in any way.
    /// </summary>
    Pinned,

    /// <summary>
    /// The asset is not included because it is not required or relevant.
    /// </summary>
    /// <remarks>
    /// For example, if no mods in the profile modify an NPC's face, then it will not (and should
    /// not) have facegen or facetint assets in the merge. This is an intended result and not the
    /// same as <see cref="Unavailable"/> which is considered an error.
    /// </remarks>
    Unused,
}
