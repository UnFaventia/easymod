using Focus.Apps.EasyNpc.Data;

namespace Focus.Apps.EasyNpc.ModManagers;

/// <summary>
/// Abstract factory for creating <see cref="IModManager"/> instances.
/// </summary>
public interface IModManagerFactory
{
    /// <summary>
    /// Creates a mod manager instance using the specified profile for configuration.
    /// </summary>
    /// <param name="profile">The selected profile.</param>
    IModManager CreateModManager(ProfileData profile);
}
