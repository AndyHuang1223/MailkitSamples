using MailKitSample02.Models;
using MailKitSample02.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailKitSample02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotifyController : ControllerBase
{
    private readonly MailKitEmailSenderService _emailSender;

    public NotifyController(MailKitEmailSenderService emailSender)
    {
        _emailSender = emailSender;
    }

    [HttpPost("send-email")]
    public async Task<IActionResult> SendEmail(SendEmailRequest request)
    {
        await _emailSender.SendEmailAsync(request);
        return NoContent();
    }
}