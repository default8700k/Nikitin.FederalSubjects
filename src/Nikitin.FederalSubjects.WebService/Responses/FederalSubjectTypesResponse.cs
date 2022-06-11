using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.WebService.Responses;

public record class FederalSubjectTypesResponse
{
    public IEnumerable<FederalSubjectTypeModel> FederalSubjectTypes { get; init; } = null!;
}
