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
    
    [HttpGet("test")]
    public IActionResult TestEndpoint()
    {
        return Ok(_gameService.GetTestMessage());
    }
}