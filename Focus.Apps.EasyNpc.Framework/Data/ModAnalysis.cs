namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// The results of analyzing a mod, including all its components and plugins.
/// </summary>
public class ModAnalysis
{
    /// <summary>
    /// The plugins that were active, and their hashes, when the analysis was created.
    /// </summary>
    /// <remarks>
    /// If any plugins in the currently-active set differ then the analysis needs to be recreated,
    /// both for the changed mod and for any direct and indirect dependencies (i.e. any other mods
    /// with this one in their masters list).
    /// </remarks>
    public Dictionary<string, int> PluginHashes { get; set; } = [];

    /// <summary>
    /// The union of all features applicable to any NPC changed by this mod.
    /// </summary>
    /// <remarks>
    /// Offers a quick, at-a-glance overview of what compatibility issues this mod might have, and
    /// any other special treatment that might be required.
    /// </remarks>
    public NpcFeatures AllFeatures { get; set; }

    /// <summary>
    /// Analysis results for individual NPCs in this mod.
    /// </summary>
    public Dictionary<string, NpcAnalysis> Npcs { get; set; } = [];
}
