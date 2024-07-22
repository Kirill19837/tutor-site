using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common;

namespace TutorPro.Application.Helpers
{
	public class UmbracoMediaHelper
	{
		private readonly UmbracoHelper _umbracoHelper;

        public UmbracoMediaHelper(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        public string GetMediaUrl(IContent content, string propertyAlias)
		{
			var jsonValue = content.GetValue<string>(propertyAlias);

			var mediaObjects = JsonConvert.DeserializeObject<List<JObject>>(jsonValue);

			if (mediaObjects != null && mediaObjects.Count > 0)
			{
				var firstObject = mediaObjects[0];

				var mediaKey = firstObject["mediaKey"]?.ToString();

				if (!string.IsNullOrEmpty(mediaKey))
				{
					var mediaItem = _umbracoHelper.Media(mediaKey);
					if (mediaItem != null)
					{
						return mediaItem.Url();
					}
				}
			}

			return "";
		}
	}
}
