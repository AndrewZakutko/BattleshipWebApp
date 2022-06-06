using Application.Handlers.Fields;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class FieldController : BaseApiController
    {
        [HttpGet("clear/{id}")]
        public async Task<IActionResult> ClearField(Guid id)
        {
            return HandleResult(await Mediator.Send(new Clear.Command { Id = id }));
        }

        [HttpGet("countShips/{id}")]
        public async Task<IActionResult> GetShipsCount(Guid id)
        {
            return HandleResult(await Mediator.Send(new CheckCount.Command { Id = id }));
        }

        [HttpGet("countShipsAlive/{id}")]
        public async Task<IActionResult> GetShipsAliveCount(Guid id)
        {
            return HandleResult(await Mediator.Send(new CheckCountAlive.Command { Id = id }));
        }

        [HttpGet("info/{id}")]
        public async Task<IActionResult> GetFieldInfo(Guid id)
        {
            return HandleResult(await Mediator.Send(new Info.Command { FieldId = id }));
        }
    }
}
