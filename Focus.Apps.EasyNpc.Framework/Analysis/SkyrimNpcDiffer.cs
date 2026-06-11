using Focus.Apps.EasyNpc.Annotations;
using Focus.Apps.EasyNpc.Data;
using Mutagen.Bethesda.Skyrim;

namespace Focus.Apps.EasyNpc.Analysis;

public class SkyrimNpcDiffer : PropertyMaskDiffer<INpcGetter, Npc.TranslationMask, NpcProperty>
{
    protected override IEnumerable<NpcProperty> SupportedProperties =>
        SupportedGames<NpcProperty>.GetValues(GameName.SkyrimSE);

    protected override Npc.TranslationMask GetMask(NpcProperty property)
    {
        return property switch
        {
            NpcProperty.AIData => new(false) { AIData = true },
            NpcProperty.AIPackages => new(false) { Packages = true },
            NpcProperty.AttackData => new(false) { Attacks = true },
            NpcProperty.AttackRace => new(false) { AttackRace = true },
            NpcProperty.AwayModelName => new(false) { FarAwayModel = true },
            // TODO: Translation mask won't get as granular as specific individual flags. Refactor
            // to either a delegate based on Mask, or a Mask + Additional Delegate.
            NpcProperty.BaseStats => new(false) { Configuration = new(true) { Flags = false } },
            NpcProperty.Class => new(false) { Class = true },
            NpcProperty.CombatOverridePackageList => new(false)
            {
                CombatOverridePackageList = true,
            },
            NpcProperty.CombatStyle => new(false) { CombatStyle = true },
            NpcProperty.CrimeFaction => new(false) { CrimeFaction = true },
            NpcProperty.DeathItem => new(false) { DeathItem = true },
            NpcProperty.DefaultOutfit => new(false) { DefaultOutfit = true },
            NpcProperty.DefaultPackageList => new(false) { DefaultPackageList = true },
            NpcProperty.DestructionData => new(false) { Destructible = true },
            NpcProperty.FaceMorphs => new(false) { FaceMorph = true },
            NpcProperty.FaceParts => new(false) { FaceParts = true },
            NpcProperty.FaceTextureSet => new(false) { HeadTexture = true },
            NpcProperty.Factions => new(false) { Factions = true },
            NpcProperty.FullName => new(false) { Name = true },
            // TODO: See above
            NpcProperty.GenderFlags => new(false) { Configuration = new(false) { Flags = true } },
            NpcProperty.GiftFilter => new(false) { GiftFilter = true },
            NpcProperty.GuardWarnOverridePackageList => new(false)
            {
                GuardWarnOverridePackageList = true,
            },
            NpcProperty.HairColor => new(false) { HairColor = true },
            NpcProperty.HeadParts => new(false) { HeadParts = true },
            NpcProperty.Height => new(false) { Height = true },
            NpcProperty.Keywords => new(false) { Keywords = true },
            NpcProperty.ObserveDeadBodyOverridePackageList => new(false)
            {
                ObserveDeadBodyOverridePackageList = true,
            },
            NpcProperty.Perks => new(false) { Perks = true },
            NpcProperty.Race => new(false) { Race = true },
            NpcProperty.ScriptInfo => new(false) { VirtualMachineAdapter = true },
            NpcProperty.ShortName => new(false) { ShortName = true },
            NpcProperty.Skills => new(false) { PlayerSkills = true },
            NpcProperty.SkinTone => new(false) { TextureLighting = true },
            NpcProperty.SleepOutfit => new(false) { SleepingOutfit = true },
            NpcProperty.SoundLevel => new(false) { SoundLevel = true },
            NpcProperty.SoundTypes => new(false) { Sound = true },
            NpcProperty.SpectatorOverridePackageList => new(false)
            {
                SpectatorOverridePackageList = true,
            },
            NpcProperty.Spells => new(false) { ActorEffect = true },
            NpcProperty.Template => new(false) { Template = true },
            NpcProperty.TintLayers => new(false) { TintLayers = true },
            NpcProperty.VoiceType => new(false) { Voice = true },
            NpcProperty.Weight => new(false) { Weight = true },
            NpcProperty.WornArmor => new(false) { WornArmor = true },
            _ => throw new NotImplementedException(
                $"Property type {property} is not available in Skyrim."
            ),
        };
    }
}
