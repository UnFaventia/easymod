using FastEnumUtility;
using Focus.Apps.EasyNpc.Data;

namespace Focus.Apps.EasyNpc.Rules;

/// <summary>
/// Provides the logic for applying the user rules in <see cref="ProfileData"/> to the current mod
/// list and load order.
/// </summary>
/// <param name="profile">The current profile containing mod and NPC rules.</param>
/// <param name="analysis">Analysis results from the mods/load order.</param>
public class ProfileRules(ProfileData profile, SessionAnalysisData analysis)
{
    private readonly SessionAnalysisData analysis = analysis;
    private readonly ProfileData profile = profile;

    /// <summary>
    /// Evaluates how the current profile and mod/load order analysis affects a given NPC.
    /// </summary>
    /// <param name="recordKey">The form key for the NPC in "Skyrim.esm:123abc" format.</param>
    /// <returns>An <see cref="NpcExplanation"/> containing details about each mod, plugin and asset
    /// that was evaluated for this NPC, which rules/exclusions applied to each, and which was
    /// ultimately chosen and why.</returns>
    public NpcExplanation Explain(string recordKey)
    {
        var result = new NpcExplanation
        {
            PluginOrder = analysis
                .PluginOrder.Where(pluginName =>
                    analysis.PluginMods.TryGetValue(pluginName, out var modKey)
                    && analysis.Mods.TryGetValue(modKey, out var modAnalysis)
                    && modAnalysis.Npcs.ContainsKey(recordKey)
                )
                .ToList(),
        };
        var previousConfig = profile.Npcs.GetValueOrDefault(recordKey);
        foreach (var (groupName, propertyGroup) in profile.PropertyGroups)
        {
            var groupExplanation = new NpcPropertyGroupExplanation();
            result.PropertyGroups.Add(groupName, groupExplanation);
            if (
                previousConfig?.PluginSources.TryGetValue(groupName, out var configSource) == true
                && configSource.Pinned
            )
            {
                groupExplanation.PluginName = configSource.PluginName;
                groupExplanation.Reason = PluginReason.Pinned;
                continue;
            }
            foreach (var modPriority in propertyGroup.ModPriorities)
            {
                if (!analysis.TryGetNpc(modPriority.ModKey, recordKey, out var npc))
                {
                    continue;
                }
                var matchedExclusionRules = GetMatchedExclusionRules(
                    npc,
                    propertyGroup.Properties,
                    modPriority.Exclusions
                );
                if (matchedExclusionRules != 0)
                {
                    groupExplanation.PreviousCandidates.Add(
                        new()
                        {
                            PluginName = npc.PluginName,
                            MatchedExclusions = matchedExclusionRules,
                        }
                    );
                }
                else
                {
                    groupExplanation.PluginName = npc.PluginName;
                    groupExplanation.Reason = PluginReason.Priority;
                    break;
                }
            }
            if (string.IsNullOrEmpty(groupExplanation.PluginName))
            {
                groupExplanation.PluginName = result.PluginOrder.FirstOrDefault() ?? "";
                groupExplanation.Reason = PluginReason.Origin;
            }
        }
        // TODO: Facegeom/facetint
        return result;
    }

    private static ModExclusionRules GetMatchedExclusionRules(
        NpcAnalysis npc,
        IEnumerable<NpcProperty> properties,
        ModExclusionRules configuredRules
    )
    {
        const PropertyDiffFlags SAME_AS_ORIGINAL_FLAGS = PropertyDiffFlags.IdenticalToFirstMaster;
        const PropertyDiffFlags SAME_AS_ANY_MASTER_FLAGS =
            PropertyDiffFlags.IdenticalToFirstMaster
            | PropertyDiffFlags.IdenticalToMiddleMaster
            | PropertyDiffFlags.IdenticalToLastMaster;
        const PropertyDiffFlags SAME_AS_ALL_MASTER_FLAGS = PropertyDiffFlags.IdenticalToAllMasters;

        var matchedRules = configuredRules;
        foreach (var property in properties)
        {
            if (!npc.PropertyDiffs.TryGetValue(property, out var diff))
            {
                // TODO: Log a warning
                continue;
            }
            if ((diff & SAME_AS_ORIGINAL_FLAGS) == 0)
            {
                matchedRules &= ~ModExclusionRules.SameAsOriginal;
            }
            if ((diff & SAME_AS_ANY_MASTER_FLAGS) == 0)
            {
                matchedRules &= ~ModExclusionRules.SameAsAnyMaster;
            }
            if ((diff & SAME_AS_ALL_MASTER_FLAGS) == 0)
            {
                matchedRules &= ~ModExclusionRules.SameAsAllMasters;
            }
            if ((diff & SAME_AS_ORIGINAL_FLAGS & ~SAME_AS_ALL_MASTER_FLAGS) == 0)
            {
                matchedRules &= ~ModExclusionRules.ReversionToOriginal;
            }
            if ((diff & SAME_AS_ANY_MASTER_FLAGS & ~SAME_AS_ALL_MASTER_FLAGS) == 0)
            {
                matchedRules &= ~ModExclusionRules.ReversionToMaster;
            }
        }
        return matchedRules;
    }
}
