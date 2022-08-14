using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Reflection;

namespace Nikitin.FederalSubjects.WebService.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class VersionController : ControllerBase
{
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Description = "Success", Type = typeof(VersionDto))]
    public IActionResult GetVersion()
    {
        var versionDto = CreateVersionDto(Assembly.GetExecutingAssembly().GetName().Version);
        return new JsonResult(versionDto);

        static VersionDto CreateVersionDto(Version? version) =>
            new()
            {
                Version = version?.ToString()
            };
    }

    public record class VersionDto
    {
        public string? Version { get; init; }
    }
}
