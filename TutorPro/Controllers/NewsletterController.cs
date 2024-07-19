using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Interfaces;
using Umbraco.Cms.Web.Common.Controllers;

namespace TutorPro.Controllers
{
    public class NewsletterController : UmbracoApiController
    {
        private readonly ISubscribeService _subscribeService;

        public NewsletterController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email, string culture)
        {
            await _subscribeService.Subscribe(email, culture);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            await _subscribeService.Unsubscribe(email);

            return Content(@"<!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <title>Unsubscribe</title>
                <script>
                    window.onload = function() {
                        setTimeout(function() {
                            window.close();
                        }, 2000);
                    }
                </script>
            </head>
            <body>
                <p>You have been unsubscribed successfully.</p>
            </body>
            </html>", "text/html");      
        }
    }
}
