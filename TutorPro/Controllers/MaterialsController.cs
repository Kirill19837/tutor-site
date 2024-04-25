using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Interfaces;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Controllers
{
    public class MaterialsController : UmbracoApiController
    {
        private readonly UmbracoHelper _umbracoHelper;
        private readonly IMaterialsService _materialsService;

        public MaterialsController(UmbracoHelper umbracoHelper, IMaterialsService materialsService)
        {
            _umbracoHelper = umbracoHelper;
            _materialsService = materialsService;
        }

        [HttpGet]
        public IActionResult GetMaterials(string searchText, string subject, string grade, string level, string sort, int page = 1, int pageSize = 12)
        {
            var materialsPage = _umbracoHelper.ContentAtRoot().DescendantsOrSelf<BlockGridPage>()
                .FirstOrDefault(x => x.ContentType.Alias == "blockGridPage" && x.Name == "Matirials");

            if (materialsPage == null || materialsPage.TBlockGridPage == null)
                return NotFound("Matirials page not found");

            return Ok(_materialsService.GetMaterials(materialsPage, searchText, subject, grade, level, sort, page, pageSize));
        }
    }
}
