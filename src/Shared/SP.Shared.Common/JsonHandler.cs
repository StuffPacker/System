using System.Text.Json;

namespace SP.Shared.Common;

public static class JsonHandler
{
    public static T Deserialize<T>(string dtoString)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var result = JsonSerializer.Deserialize<T>(dtoString, options);
        return result!;
    }
}