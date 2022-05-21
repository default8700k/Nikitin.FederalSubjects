using Microsoft.AspNetCore.Mvc;
using Nikitin.FederalSubjects.WebService.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;

namespace Nikitin.FederalSubjects.WebService.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class VersionController : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(VersionResponse))]
    public IActionResult GetVersion() =>
        new OkObjectResult(
            new VersionResponse
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            }
        );
}
