using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;

namespace TutorPro.Application.Services
{
    public class MaterialsService : IMaterialsService
    {
        
        private readonly ILogger<MaterialsService> _logger;
        public MaterialsService(ILogger<MaterialsService> logger)
        {
            _logger = logger;
        }
        public async Task<FilterResponse> GetMaterials(string searchText, string subject, string grade, string level, string sort, int page = 1, int pageSize = 12)
        {
            // Формуємо об'єкт JSON для параметра input
            var inputObject = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(searchText))
                inputObject["searchValue"] = searchText;

            if (!string.IsNullOrEmpty(subject) || !string.IsNullOrEmpty(grade) || !string.IsNullOrEmpty(level))
            {
                var tags = new List<string>();
                if (!string.IsNullOrEmpty(subject))
                    tags.Add("#"+subject.ToLower());
                if (!string.IsNullOrEmpty(grade))
                    tags.Add("#"+grade.ToLower());
                if (!string.IsNullOrEmpty(level))
                    tags.Add("#"+level.ToLower());
                inputObject["tags"] = tags.ToArray();
            }

            if (!string.IsNullOrEmpty(sort))
                inputObject["sortBy"] = sort;

            // Перетворюємо об'єкт в JSON-рядок
            var inputJson = JsonConvert.SerializeObject(inputObject);

            // Виконуємо запит до стороннього API
            var apiUrl = "https://tutorpro.team/api/trpc/availableMaterial.getPublicMaterials";
            var fullUrl = $"{apiUrl}?input={HttpUtility.UrlEncode(inputJson)}";

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(fullUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        // Отримуємо дані з відповіді API
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Result>(content);
                        var materials = result?.ResultData; // Припустимо, що є клас Material для відображення матеріалів

                        // Повертаємо отримані дані
                        return GetPaginationMaterialsList(materials, page, pageSize);
                    }
                    else
                    {
                        // Обробка помилки, якщо запит до API був неуспішним
                        var errorMessage = $"Failed to retrieve materials from API. Status code: {response.StatusCode}";
                        _logger.LogError(errorMessage);
                        throw new Exception(errorMessage);
                    }
                }
            }
        }     

        private FilterResponse GetPaginationMaterialsList(List<MaterialCardView> filteredMaterials, int page, int pageSize)
        {         
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
