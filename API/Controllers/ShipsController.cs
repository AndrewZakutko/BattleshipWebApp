using Application.Entities;
using Application.Handlers.ShipHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class ShipsController : BaseApiController
    {
        [HttpGet("/api/ships/list")]
        public async Task<IActionResult> List()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
        [HttpPost("/api/ships/add")]
        public async Task<IActionResult> Add(AddShip addShip)
        {
            return HandleResult(await Mediator.Send(new Add.Command {AddShip = addShip}));
        }
        [HttpDelete("/api/ships/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command {Id = id}));
        }
    }
}