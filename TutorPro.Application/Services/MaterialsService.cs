using Microsoft.Extensions.Logging;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly ILogger<MaterialsService> _logger;
        public MaterialsService(ILogger<MaterialsService> logger)
        {
            _logger = logger;
        }
        public FilterResponse GetMaterials(BlockGridPage materialsPage, string searchText, string subject, string grade, string level, string sort, int page = 1, int pageSize = 12)
        {
            var filteredMaterials = new List<MaterialCardView>();

            foreach (var content in materialsPage.TBlockGridPage)
            {
                if (content.Content is TMatirials matirials && matirials.TMatirialCards != null)
                {
                    foreach (var card in matirials.TMatirialCards)
                    {
                        if (card.Content is not TMatirialCard matirialCard)
                        {
                            continue;     
                        }

                        if (searchText == null || matirialCard.TTitle.ToLower().Contains(searchText.ToLower()) || matirialCard.TText.ToLower().Contains(searchText.ToLower())) // Search filter
                        {
                            // Checking filters
                            if (IsMatchFilter(matirialCard, subject, grade, level))
                            {
                                filteredMaterials.Add(new MaterialCardView
                                {
                                    Title = matirialCard.TTitle,
                                    Text = matirialCard.TText,
                                    ImageUrl = matirialCard.TImage?.Url(),
                                    Tags = matirialCard.TTags?.ToList(),
                                    LinkUrl = matirialCard.TItemLink?.Url,
                                    DateOfRealeaseUpdate = matirialCard.TDateOfReleaseUpdate,
                                    NumberOfUse = matirialCard.TNumberOfUse,
                                });
                            }
                        }
                    }

                    _logger.LogInformation("Materials picked successfully.");

                    break;
                }
            }

            return GetSortedMaterialsList(filteredMaterials, page, pageSize, sort);
        }

        private bool IsMatchFilter(TMatirialCard materialCard, string subject, string grade, string level)
        {
            // Checking if the material matches the entered filters
            return (subject == null || materialCard.TTags?.Contains(subject) == true)
                && (grade == null || materialCard.TTags?.Contains(grade) == true)
                && (level == null || materialCard.TTags?.Contains(level) == true);
        }

        private FilterResponse GetSortedMaterialsList(List<MaterialCardView> filteredMaterials, int page, int pageSize, string sort)
        {
            // Sorting
            if (sort == "Date of release/update")
            {
                filteredMaterials = filteredMaterials.OrderByDescending(m => m.DateOfRealeaseUpdate).ToList();
            }
            else if (sort == "Number of use")
            {
                filteredMaterials = filteredMaterials.OrderByDescending(m => m.NumberOfUse).ToList();
            }

            // Pagination
            var totalCount = filteredMaterials.Count;
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedMaterials = filteredMaterials.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            _logger.LogInformation("Materials filtered and sorted successfully.");

            return new FilterResponse()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Materials = paginatedMaterials
            };
        }
    }
}
