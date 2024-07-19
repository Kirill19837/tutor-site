using Microsoft.Extensions.Logging;
using System.Linq;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using TutorPro.Application.Models.ResponseModel;
using Umbraco.Cms.Core.Models.PublishedContent;
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

        public BlogResponse GetBlogs(BlogPage blogPage, string searchText,string? category, int page = 1,int pageSize = 10)
        {
            List<BlogView> viewBlogs = new List<BlogView>();
            IEnumerable<IPublishedContent> Children = blogPage.Children;
            
            if(category != null)
            {
                var categoryPage = blogPage.Children.FirstOrDefault(c => c is BlogCategoryPage categoryPage && categoryPage.TCategory == category);

                if(categoryPage != null)
                    Children = categoryPage.Children;
            }
            else
            {
                var categoryPages = blogPage.Children.OfType<BlogCategoryPage>();
                var allChildren = new List<BlogArticle>();

                foreach (var categoryPage in categoryPages)
                {
                    allChildren.AddRange(categoryPage.Children.OfType<BlogArticle>());
                }

                Children = Children.Concat(allChildren);
            }

            foreach (var blog in Children)
            {
                if (blog is not BlogArticle blogArticle || !blog.IsPublished())
                {
                    continue;
                }

                if (blogArticle != null && (searchText == null || blogArticle.TTitle.ToLower().Contains(searchText.ToLower())))
                {
                    viewBlogs.Add(new BlogView
                    {
                        Title = blogArticle.TTitle,
                        Url = blogArticle.Url(),
                        ImageUrl = blogArticle.TImage?.Url(),
                        DateTime = blogArticle.TPublicDate,
                    });
                }                                 
            }

            viewBlogs = viewBlogs.OrderByDescending(blog => blog.DateTime).ToList();

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
