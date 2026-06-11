using Focus.Apps.EasyNpc.Data;

namespace Focus.Apps.EasyNpc.ModManagers.ModOrganizer;

/// <summary>
/// Mod manager factory for Mod Organizer instances.
/// </summary>
public class ModOrganizerFactory : IModManagerFactory
{
    public IModManager CreateModManager(ProfileData profile)
    {
        // HACK: ModDirectoryPath isn't actually correct here, we are just using it temporarily for
        // testing. There needs to be an "InstancePath" instead, plus an auto-detect method.
        return new ModOrganizerModManager(profile.ModDirectoryPath);
    }
}
