using Azure.AI.TextAnalytics;

namespace Sp.Api.Business.Feature.Language;

public interface ILanguageService
{
    DetectedLanguage GetLanguage(string input);
}