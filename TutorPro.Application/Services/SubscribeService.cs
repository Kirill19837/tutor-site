using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly IContentService _contentService;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly ILogger<SubscribeService> _logger;
        private readonly IEmailSenderService _emailSenderService;
        private readonly ILocalizationService _localizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubscribeService(IContentService contentService, UmbracoHelper umbracoHelper, ILogger<SubscribeService> logger, IEmailSenderService emailSenderService, ILocalizationService localizationService, IHttpContextAccessor httpContextAccessor)
        {
            _contentService = contentService;
            _umbracoHelper = umbracoHelper;
            _logger = logger;
            _emailSenderService = emailSenderService;
            _localizationService = localizationService;
            _httpContextAccessor = httpContextAccessor;
        }
      
        public async Task Subscribe(string email, string culture)
        {
            var content = await GetNewsletterContent();
            var emailList = GetEmailList(content);

            if (emailList.Contains(email))
            {
                _logger.LogInformation("Email already exists.");
                return;
            }
            var emailWithCulture = $"{email}|{culture.ToLower()}";
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
                var message = GetDictionaryValue("New blog", "New blog", sendCulture);
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

        private string FormatCulture(string culture)
        {
            if (culture.Length > 2)
            {
                return culture.Substring(0, 2).ToLower() + "-" + culture.Substring(3).ToUpper();
            }
            return culture;
        }

        private string GetDictionaryValue(string key, string defaultValue, string culture)
        {
            var dictionaryItem = _localizationService.GetDictionaryItemByKey(key);
            if (dictionaryItem != null)
            {
                var translation = dictionaryItem.Translations.FirstOrDefault(t => t.Language.IsoCode == FormatCulture(culture));
                return translation != null ? translation.Value : defaultValue;
            }

            return defaultValue;
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

            blogView.ImageUrl = GetMediaUrl(content, "tImage");

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

        private string GetMediaUrl(IContent content, string propertyAlias)
        {
            var jsonValue = content.GetValue<string>(propertyAlias);

            var mediaObjects = JsonConvert.DeserializeObject<List<JObject>>(jsonValue);

            if (mediaObjects != null && mediaObjects.Count > 0)
            {
                var firstObject = mediaObjects[0];

                var mediaKey = firstObject["mediaKey"]?.ToString();

                if (!string.IsNullOrEmpty(mediaKey))
                {
                    var mediaItem = _umbracoHelper.Media(mediaKey);
                    if (mediaItem != null)
                    {
                        return mediaItem.Url();
                    }
                }
            }

            return "";
        }
    }
}
