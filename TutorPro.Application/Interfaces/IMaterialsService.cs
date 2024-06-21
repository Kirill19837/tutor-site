using TutorPro.Application.Models.ResponseModel;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Interfaces
{
    public interface IMaterialsService
    {
        FilterResponse GetMaterials(MaterialPage materialPage, string searchText, string subject, string grade, string level, string sort, int page = 1, int pageSize = 12);
        Task RefreshMaterialsAsync(string apiUrl, int parentId);
    }
}
