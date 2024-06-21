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
            var materialsPage = _umbracoHelper.ContentAtRoot().DescendantsOrSelf<MaterialPage>()
                .FirstOrDefault();

            if(materialsPage == null)
                return NotFound("Materials page was not found");

            var materials = _materialsService.GetMaterials(materialsPage, searchText, subject, grade, level, sort, page, pageSize);
            
            return Ok(materials);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshMaterial()
        {
            var materialsPage = _umbracoHelper.ContentAtRoot().DescendantsOrSelf<MaterialPage>()
                .FirstOrDefault();

            if (materialsPage == null || materialsPage.TMaterialFilters == null)
                return NotFound("Materials page was not found");

            var apiUrl = (materialsPage.TMaterialFilters.FirstOrDefault()?.Content as TMatirials)?.TApiUrl;

            if (apiUrl == null)
                return NotFound("Material api url was not found");

            await _materialsService.RefreshMaterialsAsync(apiUrl, materialsPage.Id);

            return Ok();
        }
    }
}
