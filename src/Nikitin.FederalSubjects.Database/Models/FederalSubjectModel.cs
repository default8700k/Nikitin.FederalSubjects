namespace Nikitin.FederalSubjects.Database.Models;

public record class FederalSubjectModel
{
    public short Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }

    public FederalDistrictModel FederalDistrict { get; init; } = null!;
    public FederalSubjectTypeModel FederalSubjectType { get; init; } = null!;
}
