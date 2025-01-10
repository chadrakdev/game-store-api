using GameStoreApi.Interfaces;

namespace GameStoreApi.Services;

public class GameService : IGameService
{
    private readonly HttpClient _httpClient;

    public GameService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> FetchGamesAsync()
    {
        var steamApiUrl = "https://api.steampowered.com/ISteamApps/GetAppList/v2/";

        var response = await _httpClient.GetAsync(steamApiUrl);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new HttpRequestException($"Status: {response.StatusCode} \nFailed to fetch data from Steam API: {response.Content}");
    }
}