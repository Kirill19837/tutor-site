using TutorPro.Application.Models.RequestModel;
using TutorPro.Application.Models.ResponseModel;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Interfaces
{
    public interface IMaterialsService
    {
        FilterResponse GetMaterials(MaterialPage materilaPage, GetMaterialsRequestModel model);
        Task RefreshMaterialsAsync(string apiUrl, int parentId);
    }
}
