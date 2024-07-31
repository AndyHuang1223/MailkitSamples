namespace MailKitSample01.Models;

public class MailServerSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public bool IsSsl { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}