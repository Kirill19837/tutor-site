using TutorPro.Application.Models;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Interfaces
{
    public interface IMaterialsService
    {
        public FilterResponse GetMaterials(BlockGridPage materialsPage, string searchText, string subject, string grade, string level, string sort, int page = 1, int pageSize = 12);
    }
}
