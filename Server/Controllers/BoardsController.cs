using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardsController : ControllerBase
{
    private readonly BoardsService _boardsService;

    public BoardsController(BoardsService boardsService)
    {
        _boardsService = boardsService;
    }

    /// <summary>
    /// Returns the current server-side state of the board
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(Common.Models.BlazorBoardData), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult Get()
    {
        var blazorBoardData = _boardsService.Get();
        if (blazorBoardData == null) { return Results.NotFound(); }
        return Results.Ok(blazorBoardData);
    }

    /// <summary>
    /// Update the server-side state of the board
    /// </summary>
    /// <param name="blazorBoardData">New board state to load to the server</param>
    [HttpPut]
    public void Put(Common.Models.BlazorBoardData blazorBoardData)
    {
        _boardsService.Put(blazorBoardData);
    }
}
