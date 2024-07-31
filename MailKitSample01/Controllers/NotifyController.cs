using MailKit.Net.Smtp;
using MailKitSample01.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace MailKitSample01.Controllers;

[Route("api/[controller]")]
public class NotifyController : ControllerBase
{
    private readonly MailServerSettings _mailServerSettings;
    private readonly ILogger<NotifyController> _logger;

    public NotifyController(ILogger<NotifyController> logger)
    {
        _logger = logger;

        _mailServerSettings = new MailServerSettings
        {
            Host = "smtp.gmail.com", // Gmail SMTP Server
            Port = 465, // Gmail SMTP Port (SSL)
            IsSsl = true, // 使用SSL
            Username = "{YOUR_GMAIL_ACCOUNT}", // Gmail帳號
            Password = "{YOUR_APPLICATION_PASSWORD}" // Gmail密碼(應用程式密碼)
        };
    }

    [HttpPost("SendEmail")]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        // 建立寄件者資訊(寄件人姓名, 寄件人信箱:會依據寄件者的信箱而定)
        var senderEmailAddress = "{YOUR_EMAIL_ADDRESS}";
        var senderMailboxAddress = new MailboxAddress("MailKit Sample Service", senderEmailAddress);

        var message = new MimeMessage();
        message.From.Add(senderMailboxAddress);
        
        message.To.Add(new MailboxAddress(request.Receiver, request.MailTo));

        message.Subject = request.Subject;
        var messageBody = new BodyBuilder
        {
            HtmlBody = request.Body
        };
        message.Body = messageBody.ToMessageBody();
       
        //Use MailKit.Net.Smtp.SmtpClient 而不是 System.Net.Mail.SmtpClient
        using var client = new SmtpClient();
        await client.ConnectAsync(_mailServerSettings.Host, _mailServerSettings.Port, _mailServerSettings.IsSsl);
        await client.AuthenticateAsync(_mailServerSettings.Username, _mailServerSettings.Password);
        var mailKitSendResult = await client.SendAsync(message);
        _logger.LogInformation("MailKit send result : {MailKitSendResult}", mailKitSendResult);
        await client.DisconnectAsync(true);
        
        // 顯示寄信結果
        return Ok(mailKitSendResult);
    }
}