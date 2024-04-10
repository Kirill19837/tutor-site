using Umbraco.Cms.Core.Sync;

namespace TutorPro.Publishing
{
    public class SchedulingPublisherServerRoleAccessor : IServerRoleAccessor
    {
        public ServerRole CurrentServerRole => ServerRole.SchedulingPublisher;
    }
}
