namespace Nikitin.FederalSubjects.Database.Models;

public record class FederalSubjectModel
{
    public short Id { get; init; }
    public short FederalDistrictId { get; init; }
    public short FederalSubjectTypeId { get; init; }
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
}
