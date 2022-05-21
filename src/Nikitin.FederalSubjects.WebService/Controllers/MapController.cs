using Microsoft.AspNetCore.Mvc;
using Nikitin.FederalSubjects.Database.Interfaces.Repositories;
using Nikitin.FederalSubjects.WebService.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Nikitin.FederalSubjects.WebService.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class MapController : ControllerBase
{
    private readonly IMapRepository _repository;

    public MapController(IMapRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MapResponse))]
    public async Task<IActionResult> GetMapAsync() =>
        new OkObjectResult(
            new MapResponse
            {
                Map = await _repository.GetMapAsync()
            }
        );
}
