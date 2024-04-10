using Umbraco.Cms.Core.Sync;

namespace TutorPro.Publishing
{
    public class SubscriberServerRoleAccessor : IServerRoleAccessor
    {
        public ServerRole CurrentServerRole => ServerRole.Subscriber;
    }
}
