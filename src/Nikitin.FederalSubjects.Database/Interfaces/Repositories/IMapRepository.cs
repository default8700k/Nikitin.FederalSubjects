using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.Database.Interfaces.Repositories;

public interface IMapRepository
{
    public Task<IEnumerable<MapModel>> GetMapAsync();
}
