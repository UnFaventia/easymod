using System.Diagnostics.CodeAnalysis;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Holds the most recent analysis results for a given profile.
/// </summary>
public class SessionAnalysisData
{
    /// <summary>
    /// Per-mod analysis results.
    /// </summary>
    /// <remarks>
    /// Each entry has a key corresponding to a mod's <see cref="ModManifest.Key"/>.
    /// </remarks>
    public Dictionary<string, ModAnalysis> Mods { get; set; } = [];

    /// <summary>
    /// Map of plugin names (<see cref="PluginManifest.Name"/>) to the mods
    /// (<see cref="ModManifest.Key"/>) they were found in.
    /// </summary>
    /// <remarks>
    /// It is possible for multiple mods to contain the same plugin and be active in the load order
    /// at the same time. The analyzer makes a best effort to "unwrap" any symlinks/hardlinks to
    /// find out which source is actually being used in a given session, and failing that, makes an
    /// educated guess based on the known mod order or file priorities.
    /// </remarks>
    public Dictionary<string, string> PluginMods { get; set; } = [];

    /// <summary>
    /// Listing order (aka load order) of all active plugins when the analysis was generated.
    /// </summary>
    public List<string> PluginOrder { get; set; } = [];

    /// <summary>
    /// Attempts to retrieve the NPC analysis for a specific NPC, from a specific mod (regardless
    /// of plugin).
    /// </summary>
    /// <param name="modKey">The <see cref="ModManifest.Key"/> to search.</param>
    /// <param name="recordKey">The record key/form key belonging to the NPC.</param>
    /// <param name="npc">The NPC analysis for the mod, if found; otherwise <c>null</c>.</param>
    /// <returns><c>true</c> if a match was found, otherwise <c>false</c>.</returns>
    public bool TryGetNpc(
        string modKey,
        string recordKey,
        [MaybeNullWhen(false)] out NpcAnalysis npc
    )
    {
        npc = null;
        return Mods.TryGetValue(modKey, out var mod) && mod.Npcs.TryGetValue(recordKey, out npc);
    }
}
