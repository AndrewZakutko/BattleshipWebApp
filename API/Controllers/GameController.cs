using Application.Entities;
using Application.EntityHalpers;
using Application.Handlers.Games;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : BaseApiController
    {
        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("historyList/{name}")]
        public async Task<IActionResult> HistoryList(string name)
        {
            return HandleResult(await Mediator.Send(new HistoryList.Command { Name = name }));
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            return HandleResult(await Mediator.Send(new Get.Command { Name = name }));
        }

        [HttpGet("changeToStarted/{gameId}")]
        public async Task<IActionResult> ChangeStatusToStarted(Guid gameId)
        {
            return HandleResult(await Mediator.Send(new ChangeToStarted.Command { GameId = gameId }));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Player player)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Player = player }));
        }

        [HttpPost("connect")]
        public async Task<IActionResult> Connect(ConnectUser connectUser)
        {
            return HandleResult(await Mediator.Send(new Connect.Command {ConnectUser = connectUser}));
        }

        [HttpPost("finish")]
        public async Task<IActionResult> Finish(FinishGame finishGame)
        {
            return HandleResult(await Mediator.Send(new Finish.Command {FinishGame = finishGame }));
        }
    }
}