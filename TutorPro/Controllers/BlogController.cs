using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using TutorPro.Application.Models.ResponseModel;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Controllers
{
    public class BlogController : UmbracoApiController 
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IBlogService _blogService;

        public BlogController(UmbracoHelper umbracoHelper, IBlogService blogService)
        {
            _umbracoHelper = umbracoHelper;
            _blogService = blogService;
        }

        public IActionResult GetBlogs(string searchText, int page = 1, int pageSize = 10)
        {
            var blogPage = _umbracoHelper.ContentAtRoot().DescendantsOrSelf<BlogPage>().FirstOrDefault();

            if (blogPage == null)
                return NotFound("Blog page was not found");

            return Ok(_blogService.GetBlogs(blogPage, searchText, page, pageSize));            
        }
    }
}
