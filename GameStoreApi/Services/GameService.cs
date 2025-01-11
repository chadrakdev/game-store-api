using GameStoreApi.Interfaces;
using GameStoreApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace GameStoreApi.Services;

public class GameService : IGameService
{
    private readonly HttpClient _httpClient;
    private readonly string _steamApiUrl = "https://api.steampowered.com/ISteamApps/GetAppList/v2/";

    public GameService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Game>> FetchGamesAsync()
    {
        try
        {
            var response = await _httpClient.GetStringAsync(_steamApiUrl);
            var steamData = JsonConvert.DeserializeObject<SteamApiResponse>(response);

            var games = steamData.Applist.Apps.Select(app => new Game
            {
                Id = app.AppId,
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

    public async Task<Game> FetchGameByIdAsync(int id)
    {
        var response = await _httpClient.GetStringAsync(_steamApiUrl);
        var steamData = JsonConvert.DeserializeObject<SteamApiResponse>(response);

        Console.WriteLine($"Requested GameID: {id}");

        var game = steamData.Applist.Apps
            .Where(app => app.AppId == id)
            .Select(app => new Game
            {
                Name = app.Name,
                Description = app.Description,
                ImageUrl = app.ImageUrl,
                Genre = app.Genre,
                Price = app.Price
            })
            .FirstOrDefault();

        return game;
    }
}