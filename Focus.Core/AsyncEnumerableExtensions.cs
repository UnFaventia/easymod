using System.Collections.ObjectModel;

namespace Focus;

public static class AsyncEnumerableExtensions
{
    public static ReadOnlyObservableCollection<T> ToObservableCollection<T>(
        this IAsyncEnumerable<T> source
    )
    {
        var collection = new ObservableCollection<T>();
        _ = PopulateCollectionAsync(collection, source);
        return new ReadOnlyObservableCollection<T>(collection);
    }

    private static async Task PopulateCollectionAsync<T>(
        ObservableCollection<T> collection,
        IAsyncEnumerable<T> items
    )
    {
        await foreach (var item in items)
            collection.Add(item);
    }
}
