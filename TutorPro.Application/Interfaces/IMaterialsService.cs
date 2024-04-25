using TutorPro.Application.Models.ResponseModel;

namespace TutorPro.Application.Interfaces
{
    public interface IMaterialsService
    {
        Task<FilterResponse> GetMaterials(string searchText, string subject, string grade, string level, string sort, int page = 1, int pageSize = 12);
    }
}
