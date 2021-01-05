using Microsoft.Extensions.Configuration;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly IConfiguration _config;

        public ContactUsRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmailMsg(ContactUsVM src)
        {

            var apiKey = _config.GetSection("SGSettings").GetValue<string>("SendGrid");
            var client = new SendGridClient(apiKey);

            var defaultEmail = _config.GetSection("SGSettings").GetValue<string>("DefaultEmail");
            var from = new EmailAddress(defaultEmail, "MKTFY CST");
            var to = new EmailAddress(defaultEmail, "MKTFY CST");

            var subject = "MKTFY Contact Us - " + src.Subject;
            var plainTextContent = $"Message from: {src.FullName} ({src.From}) \r\n {src.TextContent}";

            var htmlContent = $"<strong>Message from</strong> {src.FullName} ({src.From})" +
                $"<p>{src.TextContent}</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var result = await client.SendEmailAsync(msg);

            return result.IsSuccessStatusCode;
        }
    }
}