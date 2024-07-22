using TutorPro.Application.Interfaces;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common.PublishedModels;
namespace TutorPro.Notifications
{
    public class BlogPagePublishingHandler : INotificationAsyncHandler<ContentPublishingNotification>
    {
        private readonly ILogger<BlogPagePublishingHandler> _logger;
        private readonly ISubscribeService _subscribeService;

        public BlogPagePublishingHandler(ILogger<BlogPagePublishingHandler> logger, ISubscribeService subscribeService)
        {
            _logger = logger;
            _subscribeService = subscribeService;
        }
        public async Task HandleAsync(ContentPublishingNotification notification, CancellationToken cancellationToken)
        {
            foreach (var content in notification.PublishedEntities)
            {
                if (content.ContentType.Alias.Equals(BlogArticle.ModelTypeAlias))
                {
                    _logger.LogInformation("Blog article was picked");

                    var cultures = content.AvailableCultures;

                    foreach(var culture in cultures)
                    {
                        var isNew = content.GetValue<bool>("tNew", culture);

                        if (isNew)
                        {
                            content.SetValue("tNew", false, culture);

                            await _subscribeService.SendLetters(content, culture);
                        }
                    }
                }
            }
        }       
    }
}
