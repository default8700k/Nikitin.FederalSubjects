using FluentAssertions;
using Nikitin.FederalSubjects.Database.Models;
using Nikitin.FederalSubjects.Database.Models.DbModels;
using Nikitin.FederalSubjects.Database.Repositories;
using Testing.Shared;
using Xunit;

namespace Nikitin.FederalSubjects.Database.Tests.Repositories;

public class FederalSubjectTypeRepositoryTests
{
    private readonly FederalSubjectTypeRepository _target;

    private readonly InMemoryDbContext _dbContext = new();

    public FederalSubjectTypeRepositoryTests()
    {
        _target = new FederalSubjectTypeRepository(
            dbContext: _dbContext
        );
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetFederalSubjectTypesAsync_ShouldBeCorrect(IEnumerable<FederalSubjectType> federalSubjectTypes)
    {
        // setup
        await _dbContext.FederalSubjectTypes.AddRangeAsync(federalSubjectTypes);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _target.GetFederalSubjectTypesAsync();

        // assert
        result.Should().BeEquivalentTo(
            federalSubjectTypes.Select(x =>
                GetFederalSubjectTypeModel(x)
            )
        );
    }

    [Fact]
    public async Task GetFederalSubjectTypesAsync_ShouldBeEmpty()
    {
        // setup
        // nothing

        // act
        var result = await _target.GetFederalSubjectTypesAsync();

        // assert
        result.Should().BeEmpty();
    }

    private static FederalSubjectTypeModel GetFederalSubjectTypeModel(FederalSubjectType federalSubjectType) =>
        new()
        {
            Id = federalSubjectType.Id,
            Name = federalSubjectType.Name
        };
}
