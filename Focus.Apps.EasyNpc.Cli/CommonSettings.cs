using System.ComponentModel;
using Spectre.Console.Cli;

namespace Focus.Apps.EasyNpc.Cli;

public class CommonSettings : CommandSettings
{
    [CommandOption("-p|--profile")]
    [Description("Name of the profile to load.")]
    public string ProfileName { get; set; } = "";
}
