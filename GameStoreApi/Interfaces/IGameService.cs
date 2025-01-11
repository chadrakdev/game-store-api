using GameStoreApi.Models;

namespace GameStoreApi.Interfaces;

public interface IGameService
{
    Task<List<Game>> FetchGamesAsync();
}