using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Nikitin.FederalSubjects.Database.Tests;

public class InMemoryDbContext : AppDbContext
{
    private readonly string _databaseName;

    public InMemoryDbContext()
    {
        _databaseName = Guid.NewGuid().ToString();
    }

    public InMemoryDbContext(string databaseName)
    {
        _databaseName = databaseName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseInMemoryDatabase(_databaseName)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
}
