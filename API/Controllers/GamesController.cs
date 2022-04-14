using Application.Handlers.Game;
using Application.Handlers.Games;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GamesController : BaseApiController
    {
        private readonly IMediator _mediator;
        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameDb>>> GetGames()
        {
            return await _mediator.Send(new List.Query());
        }
    }
}