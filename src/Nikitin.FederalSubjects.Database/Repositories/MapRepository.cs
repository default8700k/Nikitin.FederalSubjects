using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Database.Interfaces.Repositories;
using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.Database.Repositories;

public class MapRepository : IMapRepository
{
    private readonly AppDbContext _dbContext;

    public MapRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MapModel>> GetMapAsync() =>
        await _dbContext.FederalSubjects
            .Include(x => x.Map)
            .Where(x =>
                x.Map.Any(f =>
                    !string.IsNullOrWhiteSpace(f.Path)
                )
            )
            .Select(x =>
                new MapModel
                {
                    FederalSubject = new()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        FederalDistrict = new()
                        {
                            Id = x.Id,
                            Name = x.Name
                        },
                        FederalSubjectType = new()
                        {
                            Id = x.Id,
                            Name = x.Name
                        }
                    },
                    Paths = x.Map.Select(f => f.Path!)
                }
            )
            .ToListAsync();
}
