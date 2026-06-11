using System.Collections;

namespace Focus.Providers.Mutagen.Tests;

public abstract class TheoryData : IEnumerable<object[]>
{
    readonly List<object[]> data = new List<object[]>();

    protected void Add(params object[] values)
    {
        data.Add(values);
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
