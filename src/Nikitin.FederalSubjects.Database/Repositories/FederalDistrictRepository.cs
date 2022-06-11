using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Database.Interfaces.Repositories;
using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.Database.Repositories;

public class FederalDistrictRepository : IFederalDistrictRepository
{
    private readonly AppDbContext _dbContext;

    public FederalDistrictRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<FederalDistrictModel>> GetFederalDistrictsAsync() =>
        await _dbContext.FederalDistricts
            .Select(x =>
                new FederalDistrictModel
                {
                    Id = x.Id,
                    Name = x.Name
                }
            )
            .ToListAsync();
}
