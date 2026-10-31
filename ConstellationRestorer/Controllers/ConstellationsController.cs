using ConstellationRestorer.DTOs;
using ConstellationRestorer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConstellationRestorer.Controllers;

[ApiController]
[Route("api/constellations")]
public class ConstellationsController : ControllerBase
{
    private readonly IConstellationRestoreService _restoreService;

    public ConstellationsController(IConstellationRestoreService restoreService)
    {
        _restoreService = restoreService;
    }

    [HttpPost("restore")]
    [ProducesResponseType(typeof(RestoreConstellationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<RestoreConstellationResponse> Restore([FromBody] RestoreConstellationRequest request)
    {
        string missingSymbols = _restoreService.GetMissingSymbols(request.Reference!, request.Damaged!);

        var response = new RestoreConstellationResponse
        {
            MissingSymbols = missingSymbols,
            IsComplete = missingSymbols.Length == 0
        };

        return Ok(response);
    }
}
