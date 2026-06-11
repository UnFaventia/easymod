using Focus.Apps.EasyNpc.Data;

namespace Focus.Apps.EasyNpc.Rules;

/// <summary>
/// Explains the decisions made for choosing an NPC's sources.
/// </summary>
public class NpcExplanation
{
    /// <summary>
    /// Explains how the face geometry (<c>meshes/.../facegeom</c> asset) was chosen.
    /// </summary>
    public AssetExplanation FaceGeometry { get; set; } = new();

    /// <summary>
    /// Explains how the face tint (<c>textures/.../facetint</c> asset) was chosen.
    /// </summary>
    public AssetExplanation FaceTint { get; set; } = new();

    /// <summary>
    /// The list of all plugins which belong to any of the mods defined in
    /// <see cref="NpcPropertyGroup.ModPriorities"/> and contain a record for the specified NPC,
    /// sorted by their listing order according to the game and/or mod manager.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These are all the plugins that <em>could</em> appear in an
    /// <see cref="NpcPropertyCandidate.PluginName"/>, but won't necessarily do so unless no
    /// higher-priority plugin was a match.
    /// </para>
    /// <para>
    /// Each property group defines its own <see cref="NpcPropertyGroup.ModPriorities"/>, so the
    /// actual order of plugins here has no effect on the order in which they will be looked at per
    /// group. The list is sorted by load order for readability, but in terms of behavior should be
    /// treated as a set.
    /// </para>
    /// </remarks>
    public List<string> PluginOrder { get; set; } = [];

    /// <summary>
    /// Explains how each of the properties in the NPC record were assigned.
    /// </summary>
    public Dictionary<string, NpcPropertyGroupExplanation> PropertyGroups { get; set; } = [];
}
