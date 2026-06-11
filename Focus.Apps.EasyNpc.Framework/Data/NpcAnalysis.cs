namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// The results of analyzing a single NPC, as defined within a single mod/component.
/// </summary>
public class NpcAnalysis
{
    /// <summary>
    /// Name of the plugin (<see cref="PluginManifest.Name"/>) that has the winning record for the
    /// specified NPC, within the analyzed mod's own components.
    /// </summary>
    /// <remarks>
    /// Typical mods will have only one plugin, but some "modular mods" may have many and this is
    /// used to point to the correct one for applying rules.
    /// </remarks>
    public string PluginName { get; set; } = "";

    /// <summary>
    /// High-level features applicable to the analyzed mod's definition of the NPC.
    /// </summary>
    public NpcFeatures Features { get; set; }

    /// <summary>
    /// The results of diffing each property against the NPC's masters.
    /// </summary>
    /// <remarks>
    /// If the mod contains multiple plugins with the same record (rare) then the corresponding
    /// entry will be for whichever comes last in the load order.
    /// </remarks>
    public Dictionary<NpcProperty, PropertyDiffFlags> PropertyDiffs { get; set; } = [];
}
