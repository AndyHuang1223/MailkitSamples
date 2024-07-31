using MailKit.Net.Smtp;
using MailKitSample02.Models;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MailKitSample02.Services;

public class MailKitEmailSenderService
{
    private readonly MailServerSettings _settings;
    private readonly ILogger<MailKitEmailSenderService> _logger;

    public MailKitEmailSenderService(IOptions<MailServerSettings> options, ILogger<MailKitEmailSenderService> logger)
    {
        _logger = logger;
        _settings = options?.Value ??
                    throw new ArgumentNullException(nameof(options), "MailServerSettings is required.");
    }


    public async Task SendEmailAsync(SendEmailRequest request)
    {
        var message = CreateMimeMessage(request);

        using var client = new SmtpClient();
        await client.ConnectAsync(_settings.Host, _settings.Port);
        await client.AuthenticateAsync(_settings.Username, _settings.Password);
        var sendResult = await client.SendAsync(message);
        _logger.LogInformation("Mailkit: send email result: {sendResult}", sendResult);
        await client.DisconnectAsync(true);
    }

    private MimeMessage CreateMimeMessage(SendEmailRequest request)
    {
        var senderName = request.SenderName ?? _settings.DefaultSenderName;
        var senderEmail = _settings.Username;
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(senderName, senderEmail));
        message.To.Add(new MailboxAddress(request.Receiver, request.ReceiverEmail));
        message.Subject = request.Subject;
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = request.Body
        };
        message.Body = bodyBuilder.ToMessageBody();
        return message;
    }
}