using Application.Entities;
using Application.Handlers.ShipHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ShipsController : BaseApiController
    {
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddShip addShip)
        {
            return HandleResult(await Mediator.Send(new Add.Command {AddShip = addShip}));
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command {Id = id}));
        }
    }
}