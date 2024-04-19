using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using Umbraco.Cms.Web.Common.Controllers;

namespace TutorPro.Controllers
{
    public class FormController(IEmailSenderService emailSenderService, ILogger<FormController> logger) : UmbracoApiController
    {
        [HttpPost]
        public async Task<ActionResult> RequestForm([FromForm] FormRequestDTO form, CancellationToken cancellation)
        {
            if(form == null)
            {
                return BadRequest();
            }

            logger.LogError("New Ex");
            throw new Exception("New Ex");

            await emailSenderService.SendEmailAsync(form, "Request", cancellation);

            return Ok();
        }
    }
}
