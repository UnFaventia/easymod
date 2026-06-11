using FastEnumUtility;
using Focus.Apps.EasyNpc.Annotations;
using VYaml.Annotations;

namespace Focus.Apps.EasyNpc.Data;

/// <summary>
/// A single property of an NPC.
/// </summary>
/// <remarks>
/// The set of members is meant to be game-independent, with <see cref="SupportedGameAttribute"/>
/// specifying, for any given member, which game(s) are applicable.
/// </remarks>
[YamlObject(NamingConvention.UpperCamelCase)]
public enum NpcProperty
{
    [Label("AIDT")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    AIData,

    [Label("PKID")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    AIPackages,

    [Label("ATKD")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    AttackData,

    [Label("ATKR")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    AttackRace,

    [Label("ANAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    AwayModelName,

    [Label("ACBS")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    BaseStats,

    [Label("CNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Class,

    [Label("ECOR")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    CombatOverridePackageList,

    [Label("ZNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    CombatStyle,

    [Label("CRIF")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    CrimeFaction,

    [Label("INAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    DeathItem,

    [Label("DOFT")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    DefaultOutfit,

    [Label("DPLT")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    DefaultPackageList,

    [Label("DEST")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    DestructionData,

    [Label("NAM9")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    FaceMorphs,

    [Label("NAMA")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    FaceParts,

    [Label("FTST")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    FaceTextureSet,

    [Label("SNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Factions,

    [Label("FULL")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    FullName,

    [Label("ACBS.F")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    GenderFlags,

    [Label("GNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    GiftFilter,

    [Label("GWOR")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    GuardWarnOverridePackageList,

    [Label("HCLF")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    HairColor,

    [Label("PNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    HeadParts,

    [Label("NAM6")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Height,

    [Label("KWDA")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Keywords,

    [Label("OCOR")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    ObserveDeadBodyOverridePackageList,

    [Label("PRKR")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Perks,

    [Label("RNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Race,

    [Label("VMAD")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    ScriptInfo,

    [Label("SHRT")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    ShortName,

    [Label("DNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Skills,

    [Label("QNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    SkinTone,

    [Label("SOFT")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    SleepOutfit,

    [Label("NAM8")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    SoundLevel,

    [Label("CSDT")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    SoundTypes,

    [Label("SPOR")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    SpectatorOverridePackageList,

    [Label("SPLO")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Spells,

    [Label("TPLT")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Template,

    [Label("TINI")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    TintLayers,

    [Label("VTCK")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    VoiceType,

    [Label("NAM7")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    Weight,

    [Label("WNAM")]
    [SupportedGame(GameName.EnderalLE)]
    [SupportedGame(GameName.EnderalSE)]
    [SupportedGame(GameName.SkyrimLE)]
    [SupportedGame(GameName.SkyrimSE)]
    [SupportedGame(GameName.SkyrimVR)]
    WornArmor,
}
