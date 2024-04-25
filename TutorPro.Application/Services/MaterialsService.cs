using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Web;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using TutorPro.Application.Models.ResponseModel;

namespace TutorPro.Application.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<MaterialsService> _logger;
        public MaterialsService(ILogger<MaterialsService> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }
        public async Task<FilterResponse> GetMaterials(string searchText, string subject, string grade, string level, string sort, int page = 1, int pageSize = 12)
        {
            // Forming a JSON object for the input parameter
            var inputObject = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(searchText))
                inputObject["searchValue"] = searchText;

            var tags = new List<string>();

            if (!string.IsNullOrEmpty(subject))
                tags.Add("#" + subject.ToLower());
            if (!string.IsNullOrEmpty(grade))
                tags.Add("#" + grade.ToLower());
            if (!string.IsNullOrEmpty(level))
                tags.Add("#" + level.ToLower());

            if (tags.Any())
                inputObject["tags"] = tags.ToArray();

            if (!string.IsNullOrEmpty(sort))
                inputObject["sortBy"] = sort;
            
            var inputJson = JsonConvert.SerializeObject(inputObject);

            // Making a request to a third-party API
            var apiUrl = "https://tutorpro.team/api/trpc/availableMaterial.getPublicMaterials";
            var fullUrl = $"{apiUrl}?input={HttpUtility.UrlEncode(inputJson)}";

            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(fullUrl);

            if(!response.IsSuccessStatusCode)
            {               
                var errorMessage = $"Failed to retrieve materials from API. Status code: {response.StatusCode}";
                _logger.LogError(errorMessage);
                throw new Exception(errorMessage);
            }

            // Getting data from the API response
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(content);
            var materials = result?.ResultData;

            if(materials == null)
                throw new Exception("Failed to deserialize response content. Response content is null or empty.");

            return GetPaginationMaterialsList(materials, page, pageSize);                  
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
