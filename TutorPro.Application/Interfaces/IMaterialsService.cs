using TutorPro.Application.Models.RequestModel;
using TutorPro.Application.Models.ResponseModel;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Interfaces
{
    public interface IMaterialsService
    {
        FilterResponse GetMaterials(IPublishedContent materilaPage, GetMaterialsRequestModel model);
        Task RefreshMaterialsAsync(string apiUrl, MaterialPage materialPage);
    }
}
