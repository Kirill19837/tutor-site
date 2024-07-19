using Umbraco.Cms.Core.Models;

namespace TutorPro.Application.Interfaces
{
    public interface ISubscribeService
    {
        Task Subscribe(string email, string culture);
        Task Unsubscribe(string email);
        Task SendLetters(IContent content, string culture);
    }
}
