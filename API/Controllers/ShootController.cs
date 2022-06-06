using Application.EntityHalpers;
using Application.Handlers.Shoots;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ShootController : BaseApiController
    {
        [HttpPost("shoot")]
        public async Task<IActionResult> TakeAShoot(TakeShoot shoot)
        {
            return HandleResult(await Mediator.Send(new Shoot.Command { Shoot = shoot }));
        }
    }
}
