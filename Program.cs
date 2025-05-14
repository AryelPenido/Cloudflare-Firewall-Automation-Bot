using CloudflareFirewallBot;
using CloudflareFirewallBot.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient()
            .AddSingleton<CloudflareService>()
            .AddSingleton<FirewallRuleService>()
            .AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();