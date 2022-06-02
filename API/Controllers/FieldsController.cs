using Application.Handlers.FieldHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class FieldsController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> ClearField(Guid id)
        {
            return HandleResult(await Mediator.Send(new Clear.Command { Id = id }));
        }

        [HttpGet("countShips/{id}")]
        public async Task<IActionResult> GetShipsCount(Guid id)
        {
            return HandleResult(await Mediator.Send(new CheckCountOfShipCells.Command { Id = id }));
        }

        [HttpGet("countShipsAlive/{id}")]
        public async Task<IActionResult> GetShipsAliveCount(Guid id)
        {
            return HandleResult(await Mediator.Send(new CheckCountOfShipCellsAlive.Command { Id = id }));
        }

        [HttpGet("fieldInfo/{id}")]
        public async Task<IActionResult> GetFieldInfo(Guid id)
        {
            return HandleResult(await Mediator.Send(new FieldInfo.Command { FieldId = id }));
        }
    }
}
