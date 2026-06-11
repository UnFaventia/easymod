namespace Focus.Apps.EasyNpc.IO;

internal static class LinkResolver
{
    public static string? Resolve(string absolutePath, string rootPath)
    {
        if (!File.Exists(absolutePath))
        {
            return null;
        }
        if (absolutePath.StartsWith(rootPath))
        {
            return absolutePath;
        }
        var linkTarget = File.ResolveLinkTarget(absolutePath, returnFinalTarget: true);
        if (linkTarget is not null)
        {
            absolutePath = linkTarget.FullName;
            if (absolutePath.StartsWith(rootPath))
            {
                return absolutePath;
            }
        }
        var hardLinks = HardLinkHelper.GetHardLinks(absolutePath);
        return hardLinks.FirstOrDefault(path => path.StartsWith(rootPath));
    }
}
