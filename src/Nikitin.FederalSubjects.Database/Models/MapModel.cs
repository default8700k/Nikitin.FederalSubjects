namespace Nikitin.FederalSubjects.Database.Models;

public record class MapModel
{
    public FederalSubjectModel FederalSubject { get; init; } = null!;
    public IEnumerable<string> Paths { get; init; } = null!;
}
