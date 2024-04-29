using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Interfaces;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Controllers
{
    public class MaterialsController : UmbracoApiController
    {
        private readonly IMaterialsService _materialsService;
        private readonly UmbracoHelper _umbracoHelper;

        public MaterialsController(IMaterialsService materialsService, UmbracoHelper umbracoHelper)
        {
            _materialsService = materialsService;
            _umbracoHelper = umbracoHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterials(string searchText, string subject, string grade, string level, string sort, int page = 1, int pageSize = 12)
        {
            var materialsPage = _umbracoHelper.ContentAtRoot().DescendantsOrSelf<BlockGridPage>()
                .FirstOrDefault(x => x.ContentType.Alias == "blockGridPage" && x.Name == "Materials");

            if(materialsPage == null)
                return NotFound("Materials page was not found");

            var materialsBlock = materialsPage.TBlockGridPage?.FirstOrDefault(p => p.Content is TMatirials);

            var apiUrl = (materialsBlock?.Content as TMatirials)?.TApiUrl;

            if (apiUrl == null)
                return NotFound("Material api url was not found");

            var materials = await _materialsService.GetMaterials(searchText, subject, grade, level, sort, apiUrl, page, pageSize);

            return Ok(materials);
        }
    }
}
