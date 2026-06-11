using Focus.Apps.EasyNpc.Data;

namespace Focus.Apps.EasyNpc.Rules;

/// <summary>
/// Explains the decision made for choosing the source of a specific <see cref="NpcPropertyGroup">
/// for a specific NPC.
/// </summary>
public class NpcPropertyGroupExplanation
{
    /// <summary>
    /// Name of the plugin from which this group's properties will be copied/merged; always
    /// corresponds to a <see cref="PluginManifest.Name"/> in the profile.
    /// </summary>
    public string PluginName { get; set; } = "";

    /// <summary>
    /// The reason why the specified <see cref="PluginName"/> was chosen.
    /// </summary>
    public PluginReason Reason { get; set; }

    /// <summary>
    /// The list of previously-considered, higher-priority plugin candidates (from
    /// <see cref="NpcExplanation.PluginOrder"/>) that were looked at and excluded based on their
    /// configured <see cref="ModPriority.Exclusions"/>.
    /// </summary>
    /// <remarks>
    /// Since each property group defines its own priority order
    /// (<see cref="NpcPropertyGroup.ModPriorities"/>), the ordering of candidates is <b>not</b>
    /// guaranteed to be the same as the <see cref="NpcExplanation.PluginOrder"/> which is simply
    /// the game's primary load order.
    /// </remarks>
    public List<NpcPropertyGroupCandidate> PreviousCandidates { get; set; } = [];
}

/// <summary>
/// Describes a candidate plugin that was considered for an individual NPC's
/// <see cref="NpcPropertyGroup"/> and excluded because of its <see cref="ModPriority.Exclusions"/>.
/// </summary>
public class NpcPropertyGroupCandidate
{
    /// <summary>
    /// The name of the plugin (from <see cref="PluginManifest.Name"/>) that was considered.
    /// </summary>
    public string PluginName { get; set; } = "";

    /// <summary>
    /// The specific rule(s) that caused it to be excluded.
    /// </summary>
    /// <remarks>
    /// Includes the rules that were actually matched, not necessarily all the rules defined. This
    /// is a subset of the original <see cref="ModPriority.Exclusions"/>.
    /// </remarks>
    public ModExclusionRules MatchedExclusions { get; set; }
}

/// <summary>
/// Explains the reason why a specific plugin was chosen for a property group.
/// </summary>
public enum PluginReason
{
    /// <summary>
    /// The plugin is the record origin; either the literal owner in the form key or, in the case of
    /// "injected" records, the first plugin in the load order to define it.
    /// </summary>
    Origin,

    /// <summary>
    /// The plugin was chosen through normal evaluation of mod priorities and exclusion rules.
    /// </summary>
    Priority,

    /// <summary>
    /// The plugin was chosen because it was pinned by the user, and not affected by load order or
    /// mod priorities in any way.
    /// </summary>
    Pinned,
}
