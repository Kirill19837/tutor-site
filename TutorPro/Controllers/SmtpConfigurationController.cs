using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using TutorPro.Application;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using Umbraco.Cms.Core.Configuration.Models;
using Umbraco.Cms.Web.Common.Controllers;

namespace TutorPro.Controllers
{
    public class SmtpConfigurationController : UmbracoApiController
    {
        private readonly IConfiguration _configuration;
        private readonly GlobalSettings _globalSettings;
        private readonly IEmailSenderService _emailService;
        private readonly ILogger<SmtpConfigurationController> _logger;

        public SmtpConfigurationController(IConfiguration configuration,
            IOptions<GlobalSettings> globalSettings, ILogger<SmtpConfigurationController> logger,
            IEmailSenderService emailService)
        {
            _configuration = configuration;
            _logger = logger;
            _emailService = emailService;
            _globalSettings = globalSettings.Value;
        }

        [HttpGet]
        public ActionResult<SmtpConfigurationDTO> Get()
        {
            var smtpSection = _configuration.GetSection(Constants.EmailConfig.MainSection);
            var smtpConfiguration = smtpSection.Get<SmtpConfigurationDTO>();

            return Ok(smtpConfiguration);
        }

        [HttpGet]
        public async Task<ActionResult> Test()
        {
            var testContactForm = new FormRequestDTO
            {
                SenderEmail = "test@gmail.com",
                SenderName = "Test",
                SenderPhone = "Test",
            };

            try
            {
                await _emailService.SendEmailAsync(testContactForm, "Test");
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(SmtpConfigurationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _globalSettings.Smtp.Username = model.Username;
                _globalSettings.Smtp.Password = model.Password;
                _globalSettings.Smtp.From = model.From;
                _globalSettings.Smtp.Host = model.Host;
                _globalSettings.Smtp.Port = model.Port;

                var filePath = Path.Combine(Environment.CurrentDirectory, Constants.AppsettingsProduction);

                if (!System.IO.File.Exists(filePath))
                {
                    filePath = Path.Combine(Environment.CurrentDirectory, Constants.Appsettings);
                }
                var jsonFileContent = System.IO.File.ReadAllText(filePath);
                var jsonObject = JObject.Parse(jsonFileContent);

                jsonObject["Umbraco"]["CMS"]["Global"]["Smtp"]["Username"] = model.Username;
                jsonObject["Umbraco"]["CMS"]["Global"]["Smtp"]["Password"] = model.Password;
                jsonObject["Umbraco"]["CMS"]["Global"]["Smtp"]["From"] = model.From;
                jsonObject["Umbraco"]["CMS"]["Global"]["Smtp"]["Host"] = model.Host;
                jsonObject["Umbraco"]["CMS"]["Global"]["Smtp"]["Port"] = model.Port;

                System.IO.File.WriteAllText(filePath, jsonObject.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Couldn't save configuration");
                return BadRequest();
            }

            return Ok();
        }
    }
}
