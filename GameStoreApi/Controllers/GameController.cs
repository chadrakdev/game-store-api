using GameStoreApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllGames()
    {
        try
        {
            var games = await _gameService.FetchGamesAsync();
            return Ok(games);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}