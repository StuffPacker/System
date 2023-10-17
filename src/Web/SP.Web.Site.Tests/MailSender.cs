using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SP.Web.Site.Features.EmailSender;

namespace SP.Web.Site.Tests;

[TestClass]
[Ignore]
public class MailSender
{
    [TestMethod]
    public async Task ShouldSendEmail()
    {
        var target = GetTarget();
        var message = "testing";
        await target.SendEmailAsync("test@test.test", "localhost test", message);
    }

    private IEmailSender GetTarget()
    {
        var emailOptions = Options.Create(new EmailOptions());
        return new SPEmailSender(emailOptions);
    }
}