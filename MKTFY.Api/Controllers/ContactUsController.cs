using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace MKTFY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContactUsController : Controller
    {

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> SendMsg([FromBody] ContactUsVM src)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, verify the data provided");
            }
            
            await SendEmailMsg(src);
            return Ok("Message delivered successfully");
        }

        public async Task<bool> SendEmailMsg(ContactUsVM src)
        {
            // SendgGrid config - using default email as sender
            var apiKey = Environment.GetEnvironmentVariable("SendGrid");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Environment.GetEnvironmentVariable("DefaultEmail"), "MKTFY CST");
            var to = new EmailAddress(Environment.GetEnvironmentVariable("DefaultEmail"), "MKTFY CST");

            var subject = "MKTFY Contact Us - " + src.Subject;
            var plainTextContent = $"Message from: {src.FullName} ({src.FullName})" +
                $"\n{src.TextContent}";

            var htmlContent = $"<strong>Message from</strong> {src.FullName} ({src.FullName})" +
                $"<p>{src.TextContent}</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
            return true;
        }
    }
}
