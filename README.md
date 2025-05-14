# 🔒 Cloudflare Firewall Automation Bot

A background service written in **C# (.NET Worker Service)** that integrates with the **Cloudflare API** to automatically detect suspicious IPs based on traffic analytics and block them with dynamic firewall rules.

## 🚀 Features

- 🔍 Monitors incoming traffic using Cloudflare Analytics.
- 📊 Analyzes top IP addresses and request volume.
- 🛡️ Automatically creates Firewall Rules for high-volume or suspicious IPs.
- ⏱️ Runs periodically (every 10 minutes) to keep protections up to date.

## 📦 Tech Stack

- .NET 8 Worker Service
- Cloudflare REST API
- Newtonsoft.Json for JSON handling
- HttpClient with dependency injection
- appsettings.json for configuration

## 🧰 Prerequisites

- .NET 8 SDK
- A valid [Cloudflare API Token](https://developers.cloudflare.com/api/)
- Your Cloudflare `Zone ID` for the target domain

## ⚙️ Configuration

Create a file named `appsettings.json` in the project root:

```json
{
  "Cloudflare": {
    "ApiToken": "your-cloudflare-api-token",
    "ZoneId": "your-zone-id"
  }
}
