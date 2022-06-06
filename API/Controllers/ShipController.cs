using Application.EntityHalpers;
using Application.Handlers.Ships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ShipController : BaseApiController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddShip addShip)
        {
            return HandleResult(await Mediator.Send(new Add.Command {AddShip = addShip}));
        }
    }
}