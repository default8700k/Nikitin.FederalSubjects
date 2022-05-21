namespace Nikitin.FederalSubjects.WebService.Options;

public record class ConnectionStringsOptions
{
    public string DefaultConnection { get; init; } = null!;
}
