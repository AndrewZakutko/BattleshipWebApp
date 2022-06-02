using Application.Entities;
using Application.Handlers.ShootHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ShootsController : BaseApiController
    {
        [HttpPost("takeAShoot")]
        public async Task<IActionResult> TakeAShoot(TakeShoot shoot)
        {
            return HandleResult(await Mediator.Send(new TakeAShoot.Command { Shoot = shoot }));
        }
    }
}
