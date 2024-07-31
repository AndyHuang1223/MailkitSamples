using System.Text.Json.Serialization;

namespace MailKitSample02.Models;

public class MailServerSettings
{
    public const string MailServerSettingsKey = "MailServerSettings";
    [JsonPropertyName("Host")] public string? Host { get; set; }
    [JsonPropertyName("Port")] public int Port { get; set; }
    [JsonPropertyName("IsSsl")] public bool IsSsl { get; set; }

    [JsonPropertyName("DefaultSenderName")]
    public string? DefaultSenderName { get; set; }

    [JsonPropertyName("Username")] public string? Username { get; set; }
    [JsonPropertyName("Password")] public string? Password { get; set; }
}