namespace Focus.Apps.EasyNpc.Cli;

/// <summary>
/// Helper for tracking the most-recent progress of an <see cref="IProgressReporter"/>.
/// </summary>
/// <remarks>
/// The assumption is that <see cref="IProgressReporter"/> may be concurrent, and events published
/// on background threads can cause unpredictable and generally undesirable effects on the
/// single-threaded console UI, or indeed any UI (although WPF etc. have dispatchers to compensate).
/// Use of this class supports a polling-based system where the caller can simply call
/// <see cref="GetLatest"/> periodically, on its own (main) thread, to update status.
/// </remarks>
public class ProgressAccumulator : IDisposable
{
    private readonly IProgressReporter reporter;
    private readonly object sync = new();

    private ProgressEventArgs? latest;

    public ProgressAccumulator(IProgressReporter reporter)
    {
        this.reporter = reporter;
        reporter.Progress += OnReporterProgress;
    }

    public void Dispose()
    {
        reporter.Progress -= OnReporterProgress;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets the latest status, defined as the one with the highest
    /// <see cref="ProgressEventArgs.ItemIndex"/> so far.
    /// </summary>
    public ProgressEventArgs? GetLatest()
    {
        // Intentionally don't lock here, since (a) it's expected to be called from the main thread
        // and we don't want to block it, and (b) if there is a race, then it makes no practical
        // difference because the reader will just get a newer value on the next tick.
        return latest;
    }

    private void OnReporterProgress(object? sender, ProgressEventArgs e)
    {
        // Tracking the "latest" item is non-trivial because the framework doesn't provide atomic
        // compare-and-update on reference types (except on the reference itself). Regardless of
        // whether "latest" means "highest index" or "last published", we're effectively left with
        // 3 choices: lock, use a ConcurrentStack/Queue, or implement an elaborate atomic udpater
        // with retries. All have their tradeoffs; the use of "naive" locking assumes, probably
        // correctly in most cases, that the operation contributing to progress is more expensive
        // than the progress update itself.
        lock (sync)
        {
            if (latest is null || e.ItemIndex > latest.ItemIndex)
            {
                latest = e;
            }
        }
    }
}
