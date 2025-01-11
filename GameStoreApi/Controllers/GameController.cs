using GameStoreApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameStoreApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGames([FromQuery] int? id)
    {
        if (id.HasValue)
        {
            var game = await _gameService.FetchGameByIdAsync(id.Value);

            if (game == null) return NotFound($"Game with ID {id.Value} not found.");

            return Ok(game);
        }
        
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