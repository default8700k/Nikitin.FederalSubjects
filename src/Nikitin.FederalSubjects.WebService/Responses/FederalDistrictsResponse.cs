using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.WebService.Responses;

public record class FederalDistrictsResponse
{
    public IEnumerable<FederalDistrictModel> FederalDistricts { get; init; } = null!;
}
