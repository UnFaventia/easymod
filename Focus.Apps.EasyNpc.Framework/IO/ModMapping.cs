using System.Diagnostics.CodeAnalysis;
using Focus.Apps.EasyNpc.Data;

namespace Focus.Apps.EasyNpc.IO;

/// <summary>
/// Helper for building a <see cref="ModMapping{T}"/>.
/// </summary>
public static class ModMapping
{
    /// <summary>
    /// Builds a new mod mapping.
    /// </summary>
    /// <param name="registryData">Registry of installed mods, as obtained from
    /// <see cref="ModManagers.IModRepository"/>.</param>
    /// <param name="rootDirectoryPath">Root directory containing all mods/components; usually
    /// obtained from <see cref="ModManagers.ModManagerConfiguration.ModRootDirectory"/>.</param>
    /// <param name="assets">The assets to map.</param>
    /// <param name="filePathSelector">Selector to map assets to absolute file paths, e.g. by
    /// prepending the game data directory path.</param>
    public static ModMapping<T> Build<T>(
        ModRegistryData registryData,
        string rootDirectoryPath,
        IEnumerable<T> assets,
        Func<T, string>? filePathSelector
    )
        where T : notnull
    {
        return ModMapping<T>.Build(registryData, rootDirectoryPath, assets, filePathSelector);
    }
}

/// <summary>
/// Utility for building a mod mapping, which tracks, for a given set of assets, which mod/component
/// is actually providing each asset.
/// </summary>
/// <remarks>
/// Resolution is attempted first using symlinks (Vortex style) and then hardlinks (Mod Organizer or
/// Vortex).
/// </remarks>
/// <typeparam name="T">Type of asset to track.</typeparam>
public class ModMapping<T>
    where T : notnull
{
    private readonly Dictionary<T, (ModManifest, ModComponent)> modsByAsset = [];

    /// <summary>
    /// Builds a new mod mapping.
    /// </summary>
    /// <param name="registryData">Registry of installed mods, as obtained from
    /// <see cref="ModManagers.IModRepository"/>.</param>
    /// <param name="rootDirectoryPath">Root directory containing all mods/components; usually
    /// obtained from <see cref="ModManagers.ModManagerConfiguration.ModRootDirectory"/>.</param>
    /// <param name="assets">The assets to map.</param>
    /// <param name="filePathSelector">Selector to map assets to absolute file paths, e.g. by
    /// prepending the game data directory path.</param>
    public static ModMapping<T> Build(
        ModRegistryData registryData,
        string rootDirectoryPath,
        IEnumerable<T> assets,
        Func<T, string>? filePathSelector
    )
    {
        filePathSelector ??= asset => asset.ToString() ?? "";
        var modsByComponentPath = (
            from mod in registryData.Mods
            from component in mod.Components
            select ((mod, component), component.Path)
        ).ToDictionary(x => x.Path, x => x.Item1);
        var mapping = new ModMapping<T>();
        foreach (var asset in assets)
        {
            var filePath = filePathSelector(asset);
            if (string.IsNullOrEmpty(filePath))
            {
                continue;
            }
            var resolvedPath = LinkResolver.Resolve(filePath, rootDirectoryPath);
            if (string.IsNullOrEmpty(resolvedPath))
            {
                continue;
            }
            var relativePath = Path.GetRelativePath(rootDirectoryPath, resolvedPath);
            var componentPath = Path.GetDirectoryName(relativePath)!;
            if (modsByComponentPath.TryGetValue(componentPath, out var modAndComponent))
            {
                mapping.modsByAsset.Add(asset, modAndComponent);
            }
        }
        return mapping;
    }

    internal ModMapping() { }

    /// <summary>
    /// Gets the mod and component associated with a specified asset.
    /// </summary>
    /// <param name="asset">The asset to look up.</param>
    /// <param name="mod">The mod providing this item, if found.</param>
    /// <param name="component">The component providing this item, if found.</param>
    /// <returns><c>true</c> if the <paramref name="asset"/> can be matched to a specific
    /// mod/component, otherwise <c>false</c>.</returns>
    public bool TryGetComponent(
        T asset,
        [MaybeNullWhen(false)] out ModManifest mod,
        [MaybeNullWhen(false)] out ModComponent component
    )
    {
        if (modsByAsset.TryGetValue(asset, out var modPair))
        {
            (mod, component) = modPair;
            return true;
        }
        mod = null;
        component = null;
        return false;
    }

    /// <summary>
    /// Gets the mod associated with a specified asset.
    /// </summary>
    /// <param name="asset">The asset to look up.</param>
    /// <param name="mod">The mod providing this item, if found.</param>
    /// <returns><c>true</c> if the <paramref name="asset"/> can be matched to a specific
    /// mod/component, otherwise <c>false</c>.</returns>
    public bool TryGetMod(T asset, [MaybeNullWhen(false)] out ModManifest mod)
    {
        return TryGetComponent(asset, out mod, out _);
    }
}
