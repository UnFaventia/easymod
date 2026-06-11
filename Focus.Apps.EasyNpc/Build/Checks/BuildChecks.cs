using Focus.Apps.EasyNpc.Profiles;

namespace Focus.Apps.EasyNpc.Build.Checks;

public interface IGlobalBuildCheck
{
    IEnumerable<BuildWarning> Run(Profile profile, BuildSettings settings);
}

public interface INpcBuildCheck
{
    IEnumerable<BuildWarning> Run(INpc npc, BuildSettings settings);
}

public interface IPreparableNpcBuildCheck : INpcBuildCheck
{
    void Prepare(Profile profile);
}
