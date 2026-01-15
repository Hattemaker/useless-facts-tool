using UselessFactsTool.Models;

namespace UselessFactsTool.Services;

public interface IFactService
{
    Task<FactResponse?> GetRandomFactAsync();
}

public class FactService : IFactService
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://uselessfacts.jsph.pl/api/v2/facts/random";

    public FactService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<FactResponse?> GetRandomFactAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var fact = System.Text.Json.JsonSerializer.Deserialize<FactResponse>(json,
                new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return fact;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching fact: {ex.Message}");
            return null;
        }
    }
}
