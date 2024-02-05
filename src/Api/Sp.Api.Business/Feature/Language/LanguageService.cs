using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Options;

namespace Sp.Api.Business.Feature.Language;

public class LanguageService : ILanguageService
{
    private readonly LanguageServiceOptions _option;

    public LanguageService(IOptions<LanguageServiceOptions> options)
    {
        _option = options.Value;
    }

    public DetectedLanguage GetLanguage(string input)
    {
       AzureKeyCredential credentials = new AzureKeyCredential(_option.Key);
       Uri endpoint = new Uri(_option.Uri);
       var client = new TextAnalyticsClient(endpoint, credentials);

       DetectedLanguage detectedLanguage = client.DetectLanguage(input);
       return detectedLanguage;
    }
}