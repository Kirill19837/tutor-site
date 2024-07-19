using TutorPro.Application.Models.ResponseModel;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Interfaces
{
    public interface IBlogService
    {
        BlogResponse GetBlogs(BlogPage blogPage, string searchText, string? category, int page = 1, int pageSize = 10);
    }
}
