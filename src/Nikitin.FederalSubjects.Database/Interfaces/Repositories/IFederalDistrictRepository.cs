using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.Database.Interfaces.Repositories;

public interface IFederalDistrictRepository
{
    public Task<IEnumerable<FederalDistrictModel>> GetFederalDistrictsAsync();
}
