namespace Focus.Apps.EasyNpc;

/// <summary>
/// Provides an event that reports progress.
/// </summary>
public interface IProgressReporter
{
    /// <summary>
    /// Event raised when progress changes.
    /// </summary>
    /// <remarks>
    /// Progress events are not guaranteed to be raised in order. It is advisable to check the
    /// <see cref="ProgressEventArgs.ItemIndex"/> value to track the most recent event.
    /// </remarks>
    event EventHandler<ProgressEventArgs>? Progress;
}
