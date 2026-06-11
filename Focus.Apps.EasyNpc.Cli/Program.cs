using Focus.Apps.EasyNpc.Cli;
using Focus.Apps.EasyNpc.Cli.Commands;
using Focus.Apps.EasyNpc.ModManagers;
using Focus.Apps.EasyNpc.ModManagers.ModOrganizer;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Spectre;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;

var logger = Log.Logger = new LoggerConfiguration()
    .WriteTo.Spectre()
    .MinimumLevel.Verbose()
    .CreateLogger();
var services = new ServiceCollection();
services.AddSingleton(logger);
services.AddSingleton<ProfileSelector>(provider =>
    new(
        provider.GetRequiredService<IAnsiConsole>(),
        Environment.SpecialFolder.LocalApplicationData,
        provider.GetRequiredService<ILogger>()
    )
);
services.AddSingleton<IModManagerFactory>(new ModOrganizerFactory());
var app = new CommandApp(new DependencyInjectionRegistrar(services));
app.Configure(config =>
{
    config.PropagateExceptions();
    config
        .AddCommand<ExplainCommand>("explain")
        .WithDescription("Provide details on how a specific NPC will be merged.")
        .WithExample("explain", "Skyrim.esm:013478")
        .WithExample("explain", "Delphine");
    config
        .AddCommand<ScanCommand>("scan")
        .WithDescription("Re-scan the mod directory and update mod information.")
        .WithExample("scan");
});
app.Run(args);
