using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using Umbraco.Cms.Web.Common.Controllers;

namespace TutorPro.Controllers
{
    public class FormController(IEmailSenderService emailSenderService) : UmbracoApiController
    {
        [HttpPost]
        public async Task<ActionResult> RequestForm([FromForm] FormRequestDTO form, CancellationToken cancellation)
        {
            if(form == null)
            {
                return BadRequest();
            }

            await emailSenderService.SendEmailAsync(form, "Request", cancellation);

            return Ok();
        }
    }
}
