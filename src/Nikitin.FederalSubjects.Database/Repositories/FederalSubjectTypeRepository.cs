using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Database.Interfaces.Repositories;
using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.Database.Repositories;

public class FederalSubjectTypeRepository : IFederalSubjectTypeRepository
{
    private readonly AppDbContext _dbContext;

    public FederalSubjectTypeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<FederalSubjectTypeModel>> GetFederalSubjectTypesAsync() =>
        await _dbContext.FederalSubjectTypes
            .Select(x =>
                new FederalSubjectTypeModel
                {
                    Id = x.Id,
                    Name = x.Name
                }
            )
            .ToListAsync();
}
