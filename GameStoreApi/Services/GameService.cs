using GameStoreApi.Interfaces;
using GameStoreApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace GameStoreApi.Services;

public class GameService : IGameService
{
    private readonly HttpClient _httpClient;

    public GameService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Game>> FetchGamesAsync()
    {
        const string steamApiUrl = "https://api.steampowered.com/ISteamApps/GetAppList/v2/";
        https://api.steampowered.com/ISteamApps/GetAppList/v2/

        try
        {
            var response = await _httpClient.GetStringAsync(steamApiUrl);
            var steamData = JsonConvert.DeserializeObject<SteamApiResponse>(response);

            var games = steamData.Applist.Apps.Select(app => new Game
            {
                Name = app.Name,
                Description = app.Description,
                ImageUrl = app.ImageUrl,
                Genre = app.Genre,
                Price = app.Price
            }).ToList();

            return games;
        }
        catch (TaskCanceledException ex)
        {
            throw new Exception("The request timed out while fetching games.", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("An error occurred while making the HTTP request.", ex);
        }
        catch (JsonSerializationException ex)
        {
            throw new Exception("An error occurred while deserializing the response.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred.", ex);
        }
    }
}