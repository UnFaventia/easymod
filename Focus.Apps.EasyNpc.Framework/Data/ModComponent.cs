using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// A single component of a mod, more commonly known as a "file" on Nexus Mods and other mod sites,
/// but translating to many files on disk.
/// </summary>
public class ModComponent
{
    /// <summary>
    /// Unique key for this component.
    /// </summary>
    /// <remarks>
    /// The key is unique across <em>all</em> mods on a given system, and has similar rules and a
    /// similar purpose to <see cref="ModManifest.Key"/>.
    /// </remarks>
    public string Key { get; set; } = "";

    /// <summary>
    /// Name of the component within the mod.
    /// </summary>
    /// <remarks>
    /// If the component was unpacked from a single archive then this is generally the name of the
    /// archive. While being mostly informational, it can help for tracking multiple versions of a
    /// file as the same <see cref="Key"/> if the <see cref="ModComponentSource.Id"/> changes based
    /// on the version but the author kept an identical file name.
    /// </remarks>
    public string Name { get; set; } = "";

    /// <summary>
    /// Path to this component's files on disk, relative to the mod search path.
    /// </summary>
    public string Path { get; set; } = "";

    /// <summary>
    /// External sources where the component or its source file can be found.
    /// </summary>
    /// <remarks>
    /// These are generally used as additional resolution keys. If a component is renamed,
    /// reinstalled under a different name, or changed in any other way that might cause its
    /// <see cref="Key"/> to be different, the sources serve as secondary keys enabling the mod to
    /// be associated with the version previously present, keeping the same <see cref="Key"/> and
    /// having its <see cref="Name"/> and <see cref="Path"/> updated instead.
    /// </remarks>
    public List<ModComponentSource> Sources { get; set; } = [];
}

/// <summary>
/// Details about where a mod component came from, or could have come from.
/// </summary>
public class ModComponentSource
{
    /// <summary>
    /// Identifies the source type or "namespace" of the component in which the <see cref="Id"/> is
    /// unique.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public ModComponentSourceType Type { get; set; }

    /// <summary>
    /// The unique ID of the component within its <see cref="Type"/>.
    /// </summary>
    public string Id { get; set; } = "";
}

/// <summary>
/// The source of a mod component, i.e. where it can be downloaded.
/// </summary>
public enum ModComponentSourceType
{
    /// <summary>
    /// An individual file on Nexus mods, which is a specific version and belongs to a specific mod.
    /// </summary>
    NexusFile = 1,
}
