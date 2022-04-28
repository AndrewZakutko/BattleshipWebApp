using Application.Entities;
using Application.Handlers.ShipHandlers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class ShipsController : BaseApiController
    {
        [HttpPost("/ships/add")]
        public async Task<ActionResult> Add(AddShip ship)
        {
            return HandleResult(await Mediator.Send(new Add.Command {Ship = ship}));
        }
    }
}