using FastEnumUtility;
using Mutagen.Bethesda;
using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Annotations;

/// <summary>
/// Games that are (possibly) supported by the tool.
/// </summary>
[YamlObject(NamingConvention.UpperCamelCase)]
public enum GameName
{
    [Label("Oblivion")]
    Oblivion = 0,

    [Label("Skyrim Legendary Edition")]
    SkyrimLE = 1,

    [Label("Skyrim Special Edition (Steam)")]
    SkyrimSE = 2,

    [Label("Skyrim Special Edition (GOG)")]
    SkyrimSEGog = 7,

    [Label("Skyrim VR")]
    SkyrimVR = 3,

    [Label("Enderal LE")]
    EnderalLE = 5,

    [Label("Enderal SE")]
    EnderalSE = 6,

    [Label("Fallout 4")]
    Fallout4 = 4,

    [Label("Fallout 4 VR")]
    Fallout4VR = 9,
}

/// <summary>
/// Extensions for the <see cref="GameName"/> enumeration.
/// </summary>
public static class GameNameExtensions
{
    /// <summary>
    /// Gets the <see cref="GameRelease"/> corresponding to a given <see cref="GameName"/>.
    /// </summary>
    public static GameRelease ToGameRelease(this GameName game)
    {
        // Explicit cast is allowed here because the enum values are specifically matched to
        // Mutagen's. Otherwise this needs to be a switch.
        return (GameRelease)game;
    }
}
