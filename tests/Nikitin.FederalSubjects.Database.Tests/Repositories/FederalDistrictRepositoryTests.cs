using FluentAssertions;
using Nikitin.FederalSubjects.Database.Models;
using Nikitin.FederalSubjects.Database.Models.DbModels;
using Nikitin.FederalSubjects.Database.Repositories;
using Testing.Shared;
using Xunit;

namespace Nikitin.FederalSubjects.Database.Tests.Repositories;

public class FederalDistrictRepositoryTests
{
    private readonly FederalDistrictRepository _target;

    private readonly InMemoryDbContext _dbContext = new();

    public FederalDistrictRepositoryTests()
    {
        _target = new FederalDistrictRepository(
            dbContext: _dbContext
        );
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetFederalDistrictsAsync_ShouldBeCorrect(IEnumerable<FederalDistrict> federalDistricts)
    {
        // setup
        await _dbContext.FederalDistricts.AddRangeAsync(federalDistricts);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _target.GetFederalDistrictsAsync();

        // assert
        result.Should().BeEquivalentTo(
            federalDistricts.Select(x =>
                GetFederalDistrictModel(x)
            )
        );
    }

    [Fact]
    public async Task GetFederalDistrictsAsync_ShouldBeEmpty()
    {
        // setup
        // nothing

        // act
        var result = await _target.GetFederalDistrictsAsync();

        // assert
        result.Should().BeEmpty();
    }

    private static FederalDistrictModel GetFederalDistrictModel(FederalDistrict federalDistrict) =>
        new()
        {
            Id = federalDistrict.Id,
            Name = federalDistrict.Name
        };
}
