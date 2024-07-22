using TutorPro.Application.Interfaces;
using Umbraco.Cms.Core.Services;

namespace TutorPro.Application.Services
{
	public class TranslationsService : ITranslationsService
	{
		private readonly ILocalizationService _localizationService;

		public TranslationsService(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

		public string GetDictionaryValue(string key, string defaultValue, string culture)
		{
			var dictionaryItem = _localizationService.GetDictionaryItemByKey(key);
			if (dictionaryItem != null)
			{
				var translation = dictionaryItem.Translations.FirstOrDefault(t => t.Language.IsoCode == FormatCulture(culture));
				return translation != null ? translation.Value : defaultValue;
			}

			return defaultValue;
		}

		private string FormatCulture(string culture)
		{
			if (culture.Length > 2)
			{
				return culture.Substring(0, 2).ToLower() + "-" + culture.Substring(3).ToUpper();
			}
			return culture;
		}
	}
}
