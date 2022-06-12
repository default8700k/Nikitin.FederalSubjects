using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.WebService.Responses;

public record class MapResponse
{
    public IEnumerable<MapModel> Map { get; init; } = null!;
}
