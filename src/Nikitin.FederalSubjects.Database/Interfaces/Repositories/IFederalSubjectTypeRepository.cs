using Nikitin.FederalSubjects.Database.Models;

namespace Nikitin.FederalSubjects.Database.Interfaces.Repositories;

public interface IFederalSubjectTypeRepository
{
    public Task<IEnumerable<FederalSubjectTypeModel>> GetFederalSubjectTypesAsync();
}
