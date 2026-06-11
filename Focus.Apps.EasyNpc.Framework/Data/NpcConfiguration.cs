using Newtonsoft.Json;
using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// Configuration of a specific NPC within a <see cref="ProfileData"/>.
/// </summary>
public class NpcConfiguration
{
    /// <summary>
    /// Specifies where to obtain the face geometry file (facegen NIF).
    /// </summary>
    [JsonProperty("facegeom")]
    [YamlMember("facegeom")]
    public NpcAssetSource FaceGeometrySource { get; set; } = new();

    /// <summary>
    /// Specifies where to obtain the face tint (texture).
    /// </summary>
    [JsonProperty("facetint")]
    [YamlMember("facetint")]
    public NpcAssetSource FaceTintSource { get; set; } = new();

    /// <summary>
    /// Map of <see cref="NpcPropertyGroup"/> keys to the source plugin used for that group.
    /// </summary>
    [JsonProperty("plugin")]
    [YamlMember("plugin")]
    public Dictionary<string, NpcPluginSource> PluginSources { get; set; } = [];

    /// <summary>
    /// Configures whether or not to ignore the NPC entirely, i.e. not include its record or assets
    /// in the merge output.
    /// </summary>
    public bool Ignore { get; set; }
}

/// <summary>
/// Describes the source of a predetermined asset, e.g. face geometry or face tint.
/// </summary>
public class NpcAssetSource
{
    /// <summary>
    /// The <see cref="ModComponent.Key"/> identifying the exact location of the source file.
    /// </summary>
    [YamlMember("modComponent")]
    public string ModComponentKey { get; set; } = "";

    /// <summary>
    /// Whether or not to pin this source, i.e. keep it pointing to the same mod even if the source
    /// of important face properties like <see cref="NpcProperty.HeadParts"/> changes.
    /// </summary>
    /// <remarks>
    /// This should normally be <c>false</c> unless using a FaceGen Override - a mod that provides
    /// FaceGen assets without any associated plugin; or if using non-standard "AIO" type mods where
    /// the plugin and the assets come from entirely different mods.
    /// </remarks>
    public bool Pinned { get; set; }
}

/// <summary>
/// Describes the source from which to obtain some of an NPC's record data.
/// </summary>
public class NpcPluginSource
{
    /// <summary>
    /// The name of the plugin (<see cref="PluginManifest.Name"/>) where the data will come from.
    /// </summary>
    [YamlMember("plugin")]
    public string PluginName { get; set; } = "";

    /// <summary>
    /// Whether or not to pin the selection. Pinned sources will always stay on their current
    /// selection even if the load order, mod priority order, mod-specific rules, etc. is changed.
    /// </summary>
    /// <remarks>
    /// In a profile that is truly mix-and-match, e.g. in which both mods A and B include NPCs X and
    /// Y, and the desired outcome is to use the attributes of mod A for NPC X and mod B for NPC Y,
    /// one of the NPC sources must be pinned (specifically, whichever one does not match the
    /// implicit priority as determined by the <see cref="NpcPropertyGroup.ModPriorities"/> within
    /// the <see cref="ProfileData.PropertyGroups"/>).
    /// </remarks>
    public bool Pinned { get; set; }
}
