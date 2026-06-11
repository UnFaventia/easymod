using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Contains information retrieved about a mod, for cross-session tracking, UI hints, etc.
/// </summary>
public class ModManifest
{
    /// <summary>
    /// Unique key for this mod.
    /// </summary>
    /// <remarks>
    /// The key is generated when a new mod is first detected and is based on whatever is known
    /// about that mod, at that time. Keys are therefore not guaranteed to be "globally" unique or
    /// consistent across multiple user profiles; they are simply our best attempt to track a single
    /// conceptual "mod" as it evolves over time, considering that mod names/paths can change, Nexus
    /// metadata isn't always available, etc.
    /// </remarks>
    public string Key { get; set; } = "";

    /// <summary>
    /// The name of the mod as decided by the author, or the most current name we know of.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Other known names for this mod, used to help find mugshots or other related assets.
    /// </summary>
    public List<string> AlternateNames { get; set; } = [];

    /// <summary>
    /// External sources where the mod can be found.
    /// </summary>
    /// <remarks>
    /// These are generally used as additional resolution keys. If a mod is renamed, reinstalled
    /// under a different name, or changed in any other way that might cause its <see cref="Key"/>
    /// to be different, the sources serve as secondary keys enabling the mod to be associated with
    /// the version previously present, keeping the same <see cref="Key"/> and having its
    /// <see cref="Name"/> and <see cref="AlternateNames"/> updated instead.
    /// </remarks>
    public List<ModSource> Sources { get; set; } = [];

    /// <summary>
    /// The components belonging to this mod.
    /// </summary>
    public List<ModComponent> Components { get; set; } = [];
}

/// <summary>
/// Details about where a mod came from, or could have come from.
/// </summary>
public class ModSource
{
    /// <summary>
    /// Identifies the source type or "namespace" of the mod in which the <see cref="Id"/> is unique.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public ModSourceType Type { get; set; }

    /// <summary>
    /// The unique ID of the mod within its <see cref="Type"/>.
    /// </summary>
    public string Id { get; set; } = "";
}

/// <summary>
/// The source of a mod, i.e. where it can be downloaded.
/// </summary>
public enum ModSourceType
{
    /// <summary>
    /// A mod page on Nexus Mods.
    /// </summary>
    NexusMod = 1,
}
