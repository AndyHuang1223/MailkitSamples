namespace MailKitSample02.Models;

public class SendEmailRequest
{
    public string? Receiver { get; set; }
    public string? ReceiverEmail { get; set; }
    public string? SenderName { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}