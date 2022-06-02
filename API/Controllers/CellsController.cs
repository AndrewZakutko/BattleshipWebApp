using Application.Handlers.CellHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class CellsController : BaseApiController
    {
        [HttpGet("{fieldId}")]
        public async Task<IActionResult> GetField(Guid fieldId)
        {
            return HandleResult(await Mediator.Send(new CellList.Command { FieldId = fieldId }));
        }
    }
}
