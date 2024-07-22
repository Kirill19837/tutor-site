﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Profiling.Internal;
using System.Web;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using TutorPro.Application.Models.ResponseModel;
using TutorPro.Application.Models.UmbracoModel;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace TutorPro.Application.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<MaterialsService> _logger;
        private readonly IContentService _contentService;

        public MaterialsService(ILogger<MaterialsService> logger, IHttpClientFactory clientFactory, IContentService contentService)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _contentService = contentService;
        }
        public  FilterResponse GetMaterials(MaterialPage materilaPage, string searchText, string subject, string grade, string level, int sort, int page = 1, int pageSize = 12)
        {
            var tags = new List<string>();

            if (!string.IsNullOrEmpty(subject))
                tags.Add("#" + subject.ToLower());
            if (!string.IsNullOrEmpty(grade))
                tags.Add("#" + grade.ToLower());
            if (!string.IsNullOrEmpty(level))
            {
                tags.Add(level);
            }

            List<MaterialCard> materilaView = new List<MaterialCard>();
            foreach(var material in materilaPage.Children)
            {
                if(material is not MaterialArticle materialArticle || !material.IsPublished())
                {
                    continue;
                }

                if(materialArticle != null && (searchText == null || materialArticle.TTitle.ToLower().Contains(searchText.ToLower())|| materialArticle.TText.ToLower().Contains(searchText.ToLower())))
                {
                    if(!tags.Any() || IsMatchFilter(materialArticle, tags))
                    {
						DateTime createdDate;
						DateTime updatedDate;

						bool isCreatedDateParsed = DateTime.TryParse(materialArticle.TCreatedDate, out createdDate);
						bool isUpdatedDateParsed = DateTime.TryParse(materialArticle.TUpdatedDate, out updatedDate);

						materilaView.Add(new MaterialCard
                        {
                            Title = materialArticle.TTitle,
                            Text = materialArticle.TText,
                            Tags = materialArticle?.TTags?.ToList(),
                            ImageUrl = materialArticle?.TImageUrl,
                            Url = materialArticle?.UrlSegment,
                            ViewsNumber = materialArticle.TViewsNumber,
                            CreatedDate = isCreatedDateParsed ? createdDate : DateTime.UtcNow,
							UpdatedDate = isUpdatedDateParsed ? updatedDate : DateTime.UtcNow,
						});
                    }                 
                }
            }

            SortBy(ref materilaView, sort);

            return GetPaginationMaterialsList(materilaView, page, pageSize);
        }

        private FilterResponse GetPaginationMaterialsList(List<MaterialCard> filteredMaterials, int page, int pageSize)
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

        private void SortBy(ref List<MaterialCard> materials, int sort)
        {
            switch (sort)
            {
                case 1:
                    materials = materials.OrderByDescending(m => m.ViewsNumber).ToList();
                    break;
                case 2:
					materials = materials.OrderByDescending(m => m.UpdatedDate).ToList();
					break;
                default:
                    break;
			}
        }

        private bool IsMatchFilter(MaterialArticle materialCard, List<string> tags)
        {
            return tags.All(tag => materialCard.TTags?.Contains(tag) == true);
        }

        public async Task RefreshMaterialsAsync(string apiUrl, int parentId)
        {
			//Get materials
			_logger.LogInformation($"Loading materials for {apiUrl}");
			var materialData = await GetMaterialList(apiUrl);

			_logger.LogInformation($"Materials received");

			//Delete all articles
			var pageIndex = 0;
            const int pageSize = 100;
            var totalChildren = long.MaxValue;

            while (pageIndex * pageSize < totalChildren)
            {
                var children = _contentService.GetPagedChildren(parentId, pageIndex, pageSize, out totalChildren);
                foreach (var child in children)
                {
                    _contentService.Delete(child);
                }
                pageIndex++;
            }

            _logger.LogInformation("Materials deleted");

			//Add new articles
			var cultures = _contentService.GetRootContent().FirstOrDefault()?.AvailableCultures;

			materialData.ForEach(material =>
			{
				IContent newContent = _contentService.Create($"{material.Title}", parentId, MaterialArticle.ModelTypeAlias); 
				newContent.SetCultureEdited(cultures);

				foreach (var culture in cultures)
				{
					newContent.SetCultureName($"{material.Title}", culture);

					newContent.SetValue("tTitle", material.Title);
					newContent.SetValue("tText", material.Text);
					string tagList = string.Join("\n", material.Tags.Select(tag => tag.ToString()));
					newContent.SetValue("tTags", tagList);
					newContent.SetValue("tImageUrl", material.ImageUrl);
					newContent.SetValue("tGuid", material.Guid);
					newContent.SetValue("tViewsNumber", material.ViewsNumber);
					newContent.SetValue("tCreatedDate", material.CreatedAt);
					newContent.SetValue("tUpdatedDate", material.UpdatedAt);
				}

				_contentService.SaveAndPublish(newContent);
			});			

			_logger.LogInformation("Materials added");
		}       

        private async Task<List<MaterialCardView>> GetMaterialList(string apiUrl)
        {
            var inputObject = new Dictionary<string, object>();

            var inputJson = JsonConvert.SerializeObject(inputObject);

            // Making a request to a third-party API
            var fullUrl = $"{apiUrl}?input={HttpUtility.UrlEncode(inputJson)}";

            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync(fullUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = $"Failed to retrieve materials from API. Status code: {response.StatusCode}";
                _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }

            // Getting data from the API response
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(content);
            var materials = result?.ResultData;

            if (materials == null)
                throw new Exception("Failed to deserialize response content. Response content is null or empty.");

            return materials;
        }
    }
}
