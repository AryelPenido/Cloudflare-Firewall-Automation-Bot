using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using CloudflareFirewallBot.Models;

namespace CloudflareFirewallBot.Services;

public class CloudflareService
{
    private readonly HttpClient _httpClient;
    private readonly CloudflareAuth _auth;

    public CloudflareService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _auth = config.GetSection("Cloudflare").Get<CloudflareAuth>();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _auth.ApiToken);
    }

    public async Task<JObject> GetTopIPsAsync()
    {
        var uri = $"https://api.cloudflare.com/client/v4/zones/{_auth.ZoneId}/analytics/dashboard?since=-30&continuous=false";
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JObject.Parse(json);
    }
}