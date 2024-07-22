using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Interfaces;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Controllers
{
    public class BlogController : UmbracoApiController 
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IBlogService _blogService;
        private readonly IVariationContextAccessor _variationContextAccessor;

        public BlogController(UmbracoHelper umbracoHelper, IBlogService blogService, IVariationContextAccessor variationContextAccessor)
        {
            _umbracoHelper = umbracoHelper;
            _blogService = blogService;
            _variationContextAccessor = variationContextAccessor;
        }

        public IActionResult GetBlogs(string searchText, string culture, string category, int page = 1, int pageSize = 10)
        {
            _variationContextAccessor.VariationContext = new VariationContext(culture);

            var rootContent = _umbracoHelper.ContentAtRoot().FirstOrDefault();

            if (rootContent == null)
                return NotFound("Root content was not found");

            var blogPage = rootContent.DescendantsOrSelf<BlogPage>().FirstOrDefault();

            if (blogPage == null)
                return NotFound("Blog page was not found");

            return Ok(_blogService.GetBlogs(blogPage, searchText,category, page, pageSize));
        }
    }
}
