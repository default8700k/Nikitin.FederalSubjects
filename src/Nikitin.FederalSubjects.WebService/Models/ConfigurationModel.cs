using Nikitin.FederalSubjects.WebService.Options;

namespace Nikitin.FederalSubjects.WebService.Models;

public record class ConfigurationModel
{
    public ConnectionStringsOptions ConnectionStrings { get; init; } = null!;
}
