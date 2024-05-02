using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using TutorPro.Application.Models.RequestModel;
using Umbraco.Cms.Web.Common.Controllers;

namespace TutorPro.Controllers
{
    public class FormController(IEmailSenderService emailSenderService, ILogger<FormController> logger, IWaitlistUserService waitlistUserService) : UmbracoApiController
    {
        [HttpPost]
        public async Task<ActionResult> RequestForm([FromBody] FormRequestDTO form, CancellationToken cancellation)
        {
            if(form == null)
            {
                logger.LogError("request form is null");
                return BadRequest();
            }

            var userModel = new AddWailtListUserModel()
            {
                Name = form.SenderName,
                Email = form.SenderEmail,
                PhoneNumber = form.SenderPhone,
                Message = form.SenderMessage,
            };

            await waitlistUserService.AddWaitlistUser(userModel);

            await emailSenderService.SendEmailAsync(form, "Request", cancellation);

            logger.LogInformation($"User {userModel.Name} - successfully added");

            return Ok();
        }     
    }
}
