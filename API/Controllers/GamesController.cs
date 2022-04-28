using Application.Entities;
using Application.Handlers.GameHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class GamesController : BaseApiController
    {
        [HttpGet("/api/games/list")]
        public async Task<IActionResult> ListGames()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
        [HttpGet("/api/games/{id}")]
        public async Task<ActionResult> GetGame(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query{GameId = id}));
        }

        [HttpPost("/api/games/create")]
        public async Task<IActionResult> Create(Player player)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Player = player }));
        }

        [HttpPost("/api/games/connect")]
        public async Task<IActionResult> Connect(ConnectUser connectUser)
        {
            return HandleResult(await Mediator.Send(new Connect.Command {ConnectUser = connectUser}));
        }

        [HttpPost("/api/games/finish")]
        public async Task<IActionResult> Finish(Game game)
        {
            return HandleResult(await Mediator.Send(new Finish.Command {Game = game}));
        }
    }
}