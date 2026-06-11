namespace Focus.Apps.EasyNpc;

/// <summary>
/// Arguments for an event describing progress of a long operation.
/// </summary>
/// <param name="itemName">Name of the current item.</param>
/// <param name="itemIndex">Zero-based index of the current item.</param>
/// <param name="itemCount">Total number of items to process.</param>
public class ProgressEventArgs(string itemName, int itemIndex, int itemCount) : EventArgs
{
    /// <summary>
    /// Name of the current item.
    /// </summary>
    public int ItemCount { get; } = itemCount;

    /// <summary>
    /// Zero-based index of the current item.
    /// </summary>
    public int ItemIndex { get; } = itemIndex;

    /// <summary>
    /// Total number of items to process.
    /// </summary>
    public string ItemName { get; } = itemName;
}
