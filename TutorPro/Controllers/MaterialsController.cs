using Microsoft.AspNetCore.Mvc;
using TutorPro.Application.Interfaces;
using Umbraco.Cms.Web.Common.Controllers;

namespace TutorPro.Controllers
{
    public class MaterialsController : UmbracoApiController
    {
        private readonly IMaterialsService _materialsService;

        public MaterialsController(IMaterialsService materialsService)
        {
            _materialsService = materialsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterials(string searchText, string subject, string grade, string level, string sort, string apiUrl, int page = 1, int pageSize = 12)
        {
            var materials = await _materialsService.GetMaterials(searchText, subject, grade, level, sort, apiUrl, page, pageSize);

            return Ok(materials);
        }
    }
}
