using Focus.Apps.EasyNpc.Data;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins.Records;

namespace Focus.Apps.EasyNpc.Analysis;

public class SessionAnalyzer<TMod, TNpc>(
    IGameEnvironment<TMod> environment,
    IPropertyDiffer<TNpc, NpcProperty> differ,
    ProfileData profile
) : IProgressReporter
    where TMod : class, IModGetter
    where TNpc : class, IMajorRecordGetter, IMajorRecordQueryableGetter
{
    public event EventHandler<ProgressEventArgs>? Progress;

    private readonly IPropertyDiffer<TNpc, NpcProperty> differ = differ;
    private readonly ProfileData profile = profile;

    public async Task<SessionAnalysisData> AnalyzeAsync()
    {
        var npcIds = environment.LinkCache.AllIdentifiers<TNpc>().ToList();
        int currentIndex = -1;
        await Parallel.ForEachAsync(
            npcIds,
            (npcId, cancellationToken) =>
                new ValueTask(
                    Task.Run(
                        () =>
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            var index = Interlocked.Increment(ref currentIndex);
                            var npcRecords = environment.LinkCache.ResolveAllSimpleContexts<TNpc>(
                                npcId.FormKey
                            );
                            Progress?.Invoke(
                                this,
                                new(npcId.EditorID ?? npcId.FormKey.ToString(), index, npcIds.Count)
                            );
                        },
                        cancellationToken
                    )
                )
        );
        return new();
    }
}
