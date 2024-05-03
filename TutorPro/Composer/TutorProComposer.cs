using TutorPro.Notifications;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Notifications;

namespace TutorPro.Composer
{
    public class TutorProComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddNotificationAsyncHandler<UmbracoApplicationStartedNotification, ApplicationNotiMigration>();
        }
    }
}
