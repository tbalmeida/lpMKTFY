using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.ViewModels;
using System.Threading.Tasks;

namespace MKTFY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsController(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<bool>> SendEmailMsg([FromBody] ContactUsVM src)
        {
            // SendgGrid config - using default email as sender
            var result = await _contactUsRepository.SendEmailMsg(src);

            if (result)
            {
                return Ok("Message sent, thank you for your contact");
            }
            else
            {
                return BadRequest("Message not sent, sorry");
            }
        }
    }
}
