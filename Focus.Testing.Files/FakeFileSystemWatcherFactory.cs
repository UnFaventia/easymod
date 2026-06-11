using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;

namespace Focus.Testing.Files;

public class FakeFileSystemWatcherFactory : IFileSystemWatcherFactory
{
    public IFileSystem FileSystem => throw new NotImplementedException();
    public IEnumerable<FakeFileSystemWatcher> Watchers => watchers;

    private readonly List<FakeFileSystemWatcher> watchers = new();

    public IFileSystemWatcher CreateNew()
    {
        return New();
    }

    public IFileSystemWatcher CreateNew(string path)
    {
        return New(path);
    }

    public IFileSystemWatcher CreateNew(string path, string filter)
    {
        return New(path, filter);
    }

    public IFileSystemWatcher New()
    {
        var watcher = new FakeFileSystemWatcher();
        watchers.Add(watcher);
        return watcher;
    }

    public IFileSystemWatcher New(string path)
    {
        var watcher = new FakeFileSystemWatcher { Path = path };
        watchers.Add(watcher);
        return watcher;
    }

    public IFileSystemWatcher New(string path, string filter)
    {
        var watcher = new FakeFileSystemWatcher { Path = path, Filter = filter };
        watchers.Add(watcher);
        return watcher;
    }

    [return: NotNullIfNotNull(nameof(fileSystemWatcher))]
    public IFileSystemWatcher Wrap(FileSystemWatcher? fileSystemWatcher)
    {
        throw new NotImplementedException();
    }
}
