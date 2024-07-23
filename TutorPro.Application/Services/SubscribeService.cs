using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using TutorPro.Application.Helpers;

namespace TutorPro.Application.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly IContentService _contentService;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly UmbracoMediaHelper _umbracoMediaHelper;
		private readonly ILogger<SubscribeService> _logger;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ITranslationsService _translationsService;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubscribeService(IContentService contentService, UmbracoHelper umbracoHelper, ILogger<SubscribeService> logger, IEmailSenderService emailSenderService, ITranslationsService translationsService, IHttpContextAccessor httpContextAccessor, UmbracoMediaHelper umbracoMediaHelper)
        {
            _contentService = contentService;
            _umbracoHelper = umbracoHelper;
            _logger = logger;
            _emailSenderService = emailSenderService;
            _translationsService = translationsService;
            _httpContextAccessor = httpContextAccessor;
            _umbracoMediaHelper = umbracoMediaHelper;
        }
      
        public async Task Subscribe(string email, string culture)
        {
            var content = await GetNewsletterContent();
            var emailList = GetEmailList(content);
			var emailWithCulture = $"{email}|{culture.ToLower()}";

			if (emailList.Contains(emailWithCulture))
            {
                _logger.LogInformation("Email already exists.");
                return;
            }
            
            emailList.Add(emailWithCulture);
            await UpdateEmailList(content, emailList);

            _logger.LogInformation("Email successfully added.");
        }

        public async Task Unsubscribe(string email)
        {
            var content = await GetNewsletterContent();
            var emailList = GetEmailList(content);

            if (!emailList.Contains(email))
            {
                _logger.LogInformation("Email does not exist.");
                return;
            }

            emailList.Remove(email);
            await UpdateEmailList(content, emailList);

            _logger.LogInformation("Email successfully removed.");
        }

        public async Task SendLetters(IContent content, string sendCulture)
        {
            var newsletterContent = await GetNewsletterContent();
            var emailList = GetEmailList(newsletterContent);

            var emailDictionary = new Dictionary<string, List<string>>();

            foreach (var item in emailList)
            {
                var parts = item.Split('|');
                if (parts.Length != 2)
                {
                    _logger.LogWarning($"Invalid email format: {item}");
                    continue;
                }

                var email = parts[0];
                var culture = parts[1];

                if (!emailDictionary.ContainsKey(culture))
                {
                    emailDictionary[culture] = new List<string>();
                }

                emailDictionary[culture].Add(email);
            }

            if (emailDictionary.TryGetValue(sendCulture, out var emailsToSend))
            {
                var request = _httpContextAccessor.HttpContext?.Request;
                var domainName = $"{request.Scheme}://{request.Host}";
                var message = _translationsService.GetDictionaryValue("New blog", "New blog", sendCulture);
                var blogView = CreateBlogView(content, domainName, sendCulture);
               
                foreach(var email in emailsToSend)
                {
                    await _emailSenderService.SendBlogArticleEmailAsync(email, blogView, message, sendCulture);
                }              
            }
            else
            {
                _logger.LogWarning($"No emails found for culture: {sendCulture}");
            }
        }
       
        private NewBlogView CreateBlogView(IContent content, string domainName, string culture)
        {
            var blogView = new NewBlogView
            {
                Title = content.GetValue<string>("tTitle", culture),
            };

            var publicDateString = content.GetValue<string>("tPublicDate", culture);
            if (publicDateString == null || string.IsNullOrEmpty(publicDateString))
                blogView.DateTime = content.UpdateDate;
            else
                blogView.DateTime = DateTime.Parse(publicDateString);

            blogView.ImageUrl = _umbracoMediaHelper.GetMediaUrl(content, "tImage");

            blogView.Url = domainName + _umbracoHelper.Content(content.Id)?.Url(culture);
            blogView.UnsubscribeUrl = domainName + Constants.ApiPaths.UnsubscribeApiPath;
            blogView.SiteUrl = domainName + _umbracoHelper.ContentAtRoot().FirstOrDefault()?.Url(culture);

            return blogView;
        }

        private async Task<IContent> GetNewsletterContent()
        {
            var parent = _umbracoHelper.ContentAtRoot().DescendantsOrSelf<NewsletterOptions>().FirstOrDefault();

            if (parent == null)
            {
                _logger.LogError("An error occurred because the parent item was not found.");
                throw new Exception("An error occurred because the parent item was not found.");
            }

            var content = _contentService.GetById(parent.Id);

            if (content == null)
            {
                _logger.LogError("An error occurred because the content in newsletter was not found.");
                throw new Exception("An error occurred because the content in newsletter was not found.");
            }

            return content;
        }

        private List<string> GetEmailList(IContent content)
        {
            return content.GetValue<string>("TSenderEmails")?
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList() ?? new List<string>();
        }

        private async Task UpdateEmailList(IContent content, List<string> emailList)
        {
            content.SetValue("TSenderEmails", string.Join("\n", emailList));
            _contentService.SaveAndPublish(content);
        }       
    }
}
