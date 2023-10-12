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

    [HttpGet]
    [ProducesResponseType(typeof(Common.Models.BlazorBoardData), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult Get()
    {
        var blazorBoardData = _boardsService.Get();
        if (blazorBoardData == null) { return Results.NotFound(); }
        return Results.Ok(blazorBoardData);
    }

    [HttpPut]
    public void Put()
    {
        throw new NotImplementedException();
    }
}
