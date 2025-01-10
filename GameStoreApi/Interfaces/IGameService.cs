namespace GameStoreApi.Interfaces;

public interface IGameService
{
    Task<String> FetchGamesAsync();
}