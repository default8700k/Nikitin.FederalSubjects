using Microsoft.AspNetCore.Mvc;
using Nikitin.FederalSubjects.Database.Interfaces.Repositories;
using Nikitin.FederalSubjects.WebService.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace Nikitin.FederalSubjects.WebService.Controllers;

[ApiController]
[Produces("application/json")]
public class DataController
{
    [HttpGet]
    [Route("federal_districts")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FederalDistrictsResponse))]
    public async Task<IActionResult> GetFederalDistrictsAsync([FromServices] IFederalDistrictRepository repository) =>
        new OkObjectResult(
            new FederalDistrictsResponse
            {
                FederalDistricts = await repository.GetFederalDistrictsAsync()
            }
        );

    [HttpGet]
    [Route("federal_subject_types")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(FederalSubjectTypesResponse))]
    public async Task<IActionResult> GetFederalSubjectTypesAsync([FromServices] IFederalSubjectTypeRepository repository) =>
        new OkObjectResult(
            new FederalSubjectTypesResponse
            {
                FederalSubjectTypes = await repository.GetFederalSubjectTypesAsync()
            }
        );

    [HttpGet]
    [Route("map")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(MapResponse))]
    public async Task<IActionResult> GetMapAsync([FromServices] IMapRepository repository) =>
        new OkObjectResult(
            new MapResponse
            {
                Map = await repository.GetMapAsync()
            }
        );
}
