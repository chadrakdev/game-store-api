using GameStoreApi.Interfaces;

namespace GameStoreApi.Services;

public class GameService : IGameService
{
    public string GetTestMessage()
    {
        return "Test endpoint is working";
    }
}