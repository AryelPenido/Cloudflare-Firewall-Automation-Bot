using CloudflareFirewallBot.Services;

namespace CloudflareFirewallBot;

public class Worker(CloudflareService cfService, FirewallRuleService fwService, ILogger<Worker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Verifying traffic ...");

            var data = await cfService.GetTopIPsAsync();
            var topIPs = data["result"]?["top_ips"]?.Take(5);

            if (topIPs != null)
            {
                foreach (var ip in topIPs)
                {
                    var address = ip["ip"]?.ToString();
                    var requests = int.Parse(ip["requests"]?.ToString() ?? "0");

                    if (requests <= 1000) continue;
                    logger.LogWarning("IP {Address} made {Requests} requests. Blocking...", address, requests);
                    await fwService.CreateBlockRuleAsync(address);
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}