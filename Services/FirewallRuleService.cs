using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json.Linq;
using CloudflareFirewallBot.Models;

namespace CloudflareFirewallBot.Services;

public class FirewallRuleService
{
    private readonly HttpClient _httpClient;
    private readonly CloudflareAuth _auth;

    public FirewallRuleService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _auth = config.GetSection("Cloudflare").Get<CloudflareAuth>();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.ApiToken);
    }

    public async Task CreateBlockRuleAsync(string? ip)
    {
        var uri = $"https://api.cloudflare.com/client/v4/zones/{_auth.ZoneId}/firewall/rules";

        var rule = new
        {
            action = "block",
            filter = new
            {
                expression = $"ip.src eq \"{ip}\"",
                paused = false,
                description = $"Auto-block for IP {ip}"
            },
            description = $"Auto-blocked IP: {ip}"
        };

        var json = JArray.FromObject(new[] { rule });
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();
    }
}