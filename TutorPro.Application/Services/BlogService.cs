using Microsoft.Extensions.Logging;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using TutorPro.Application.Models.ResponseModel;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly ILogger<BlogService> _logger;
        public BlogService(ILogger<BlogService> logger)
        {
            _logger = logger;
        }

        public BlogResponse GetBlogs(BlogPage blogPage, string searchText, int page = 1,int pageSize = 10)
        {
            List<BlogView> viewBlogs = new List<BlogView>();
            foreach (var blog in blogPage.Children)
            {
                if (blog is not BlogArticle blogArticle)
                {
                    continue;
                }

                if (blogArticle != null && (searchText == null || blogArticle.TTitle.ToLower().Contains(searchText.ToLower())))
                {
                    viewBlogs.Add(new BlogView
                    {
                        Title = blogArticle.TTitle,
                        Url = blogArticle.UrlSegment,
                        ImageUrl = blogArticle.TImage?.Url(),
                        DateTime = blogArticle.UpdateDate,
                    });
                }                                 
            }

            return GetPaginationBlogsList(viewBlogs, page, pageSize);
        }

        private BlogResponse GetPaginationBlogsList(List<BlogView> filteredBlogs, int page, int pageSize)
        {
            // Pagination
            var totalCount = filteredBlogs.Count;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedBlogs = filteredBlogs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            _logger.LogInformation("Blogs filtered and sorted successfully.");

            return new BlogResponse()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Blogs = paginatedBlogs
            };
        }
    }
}
