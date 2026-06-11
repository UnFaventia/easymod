using Spectre.Console;

namespace Focus.Apps.EasyNpc.Cli;

internal static class StatusContextExtensions
{
    public static void SetIndeterminateStatus(this StatusContext context, string text)
    {
        context.Status = $"[olive]{text.EscapeMarkup()}[/]";
    }

    public static void SetProgress(
        this StatusContext context,
        string prefix,
        ProgressEventArgs progress
    )
    {
        var escapedPrefix = !string.IsNullOrEmpty(prefix) ? prefix.EscapeMarkup() + " " : "";
        context.Status =
            $"{escapedPrefix}({progress.ItemIndex}/{progress.ItemCount}): [aqua]{progress.ItemName.EscapeMarkup()}[/]";
    }
}
