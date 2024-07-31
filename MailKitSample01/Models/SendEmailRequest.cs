namespace MailKitSample01.Models;

public class SendEmailRequest
{
    public string? Receiver { get; set; }
    public string? MailTo { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}