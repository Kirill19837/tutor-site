namespace TutorPro.Application.Interfaces
{
	public interface ITranslationsService
	{
		string GetDictionaryValue(string key, string defaultValue, string culture);
	}
}
