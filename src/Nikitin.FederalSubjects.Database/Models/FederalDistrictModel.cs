namespace Nikitin.FederalSubjects.Database.Models;

public record class FederalDistrictModel
{
    public short Id { get; init; }
    public string Name { get; init; } = null!;
}
