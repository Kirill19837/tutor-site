using TutorPro.Application.Models;

namespace TutorPro.Application.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(FormRequestDTO formRequest, string subject, CancellationToken cancellation = default, bool isHtml = true);
        Task SendBlogArticleEmailAsync(string email, NewBlogView blogArticle, string subject, string culture, CancellationToken cancellation = default, bool isHtml = true);
    }
}
