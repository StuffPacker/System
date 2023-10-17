using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace SP.Web.Site.Features.EmailSender;

public class SPEmailSender : IEmailSender
{
    private readonly EmailOptions _emailOptions;
    private readonly SmtpClient _smtp;
    private MailAddress _fromAddress;

    public SPEmailSender(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
        _fromAddress = new MailAddress("stuffpacker23@gmail.com", "Stuff Packer");

        // const string fromPassword = "mhfq zhzr eaez bjxy";
        _smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Credentials = new NetworkCredential(_fromAddress.Address, _emailOptions.Password),
            Timeout = 20000
        };
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        await Task.Delay(1);
        var toAddress = new MailAddress(email);

        using (var message = new MailMessage(_fromAddress, toAddress)
               {
                   Subject = subject,
                   Body = htmlMessage,
                   IsBodyHtml = true
               })
        {
            _smtp.Send(message);
        }
    }
}