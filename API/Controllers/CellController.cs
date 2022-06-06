using Application.Handlers.Cells;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class CellController : BaseApiController
    {
        [HttpGet("list/{fieldId}")]
        public async Task<IActionResult> GetField(Guid fieldId)
        {
            return HandleResult(await Mediator.Send(new List.Command { FieldId = fieldId }));
        }
    }
}
