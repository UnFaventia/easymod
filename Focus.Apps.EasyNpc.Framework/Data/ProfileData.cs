using Focus.Apps.EasyNpc.Annotations;
using Newtonsoft.Json;
using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Data format for a session profile.
/// </summary>
[YamlObject]
public partial class ProfileData
{
    /// <summary>
    /// Unique name for this profile. Determined by the file name.
    /// </summary>
    [JsonIgnore]
    [YamlIgnore]
    public string Name { get; set; } = "";

    /// <summary>
    /// The game that this profile targets.
    /// </summary>
    [YamlMember("game")]
    public GameName GameName { get; set; } = GameName.SkyrimSE;

    /// <summary>
    /// Path to the game directory, if targeting a path different from the normal installation
    /// directory and/or the mod manager's target directory.
    /// </summary>
    [YamlMember("gamePath")]
    public string? GameDirectoryPath { get; set; } = null;

    /// <summary>
    /// Path to the root directory for mods.
    /// </summary>
    [YamlMember("modPath")]
    public string ModDirectoryPath { get; set; } = "";

    /// <summary>
    /// Default merging behaviors for mods that do not override them.
    /// </summary>
    [YamlMember("behavior")]
    public MergeBehaviors DefaultBehaviors { get; set; } = new();

    /// <summary>
    /// Configures how the build output will be generated.
    /// </summary>
    [YamlMember("output")]
    public OutputSettings OutputSettings { get; set; } = new();

    /// <summary>
    /// Configurations and overrides for individual mods.
    /// </summary>
    public Dictionary<string, ModConfiguration> Mods { get; set; } = [];

    /// <summary>
    /// Configures which NPC properties (<see cref="NpcProperty"/>) will be merged together, and the
    /// merging and priority settings for each group.
    /// </summary>
    public Dictionary<string, NpcPropertyGroup> PropertyGroups { get; set; } = [];

    /// <summary>
    /// Configurations for individual NPCs, by their form key (e.g. "Skyrim.esm:123abc").
    /// </summary>
    /// <remarks>
    /// Only entries with pinned attributes (<see cref="NpcAssetSource.Pinned"/> or
    /// <see cref="NpcPluginSource.Pinned"/>) are guaranteed to be used; the rest are determined by
    /// the rules in <see cref="PropertyGroups"/> at build time.
    /// </remarks>
    public Dictionary<string, NpcConfiguration> Npcs { get; set; } = [];
}

/// <summary>
/// Specifies the default merge behaviors for a profile, when not overridden in a particular mod.
/// </summary>
[YamlObject]
public partial class MergeBehaviors
{
    /// <summary>
    /// Merging behavior for NPC default outfits with custom armor pieces.
    /// </summary>
    public CustomOutfitBehavior CustomOutfit { get; set; } = CustomOutfitBehavior.SameAsWornArmor;

    /// <summary>
    /// Merging behavior for NPC custom races.
    /// </summary>
    public CustomRaceBehavior CustomRace { get; set; } = CustomRaceBehavior.Patch;

    /// <summary>
    /// Merging behavior for NPC custom face tints.
    /// </summary>
    public FaceTintBehavior FaceTint { get; set; } = FaceTintBehavior.Keep;

    /// <summary>
    /// Merging behavior for NPC custom worn armors (skins, wigs, etc.).
    /// </summary>
    public WornArmorBehavior WornArmor { get; set; } = WornArmorBehavior.Clone;
}

/// <summary>
/// Profile-specific build output settings.
/// </summary>
[YamlObject]
public partial class OutputSettings
{
    /// <summary>
    /// Format string for the output directory (generated mod name).
    /// </summary>
    /// <remarks>
    /// Valid tokens include:
    /// <list type="bullet">
    /// <item><b>Date</b> - the current calendar date.</item>
    /// <item><b>Profile</b> - name of the running profile.</item>
    /// </list>
    /// </remarks>
    public string ModNameFormat { get; set; } = "EasyNPC Merge {Date}";

    /// <summary>
    /// Whether to include the Player NPC record.
    /// </summary>
    /// <remarks>
    /// Having this record in the merge often tends to cause crashes and other problems, and should
    /// rarely be necessary or desired, so the default is <c>false</c>. However, the game data does
    /// not have a literal <c>Player</c> flag, so it is still possible for the player record to be
    /// included if mods change the editor ID or otherwise obfuscate which record is the player,
    /// whether intentional or not.
    /// </remarks>
    public bool IncludePlayer { get; set; }

    /// <summary>
    /// Whether to include NPCs whose merged record won't change any details about them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This condition is subject to heuristics. Simply checking against the originating record is
    /// usually incorrect because if a plugin like USSEP, AI Overhaul, etc. is the last one in a
    /// load order to modify it, then the typical user would consider this to be "unchanged" - that
    /// is, the merge isn't doing anything useful since they are going to keep those plugins enabled
    /// anyway.
    /// </para>
    /// <para>
    /// Without requiring a laborious and potentially ambiguous and conflict-ridden definition of
    /// "intended masters", the logic will normally be to look at the actual masters of the merge
    /// plugin, and compare the merged NPC to that of the last master to modify it. It is possible,
    /// though uncommon, that doing this for every NPC might ultimately cause that master to be
    /// removed, which is considered an expected outcome, because at that point it is known that the
    /// user intended to keep the mod enabled.
    /// </para>
    /// </remarks>
    public bool IncludeUnchangedNpcs { get; set; }

    /// <summary>
    /// Whether to pack assets into an archive (BSA).
    /// </summary>
    public bool EnableArchiving { get; set; } = true;
}
