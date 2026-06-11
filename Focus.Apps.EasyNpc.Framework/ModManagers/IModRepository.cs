using Focus.Apps.EasyNpc.Data;

namespace Focus.Apps.EasyNpc.ModManagers;

/// <summary>
/// Provides methods to retrieve information about installed mods.
/// </summary>
public interface IModRepository
{
    /// <summary>
    /// Gets the current mod registry information, including installed mods, components, orders
    /// and configurations.
    /// </summary>
    Task<ModRegistryData> GetModRegistryAsync();
}
