using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Nikitin.FederalSubjects.Database.Models;
using Nikitin.FederalSubjects.Database.Models.DbModels;
using Nikitin.FederalSubjects.Database.Repositories;
using Testing.Shared;
using Xunit;

namespace Nikitin.FederalSubjects.Database.Tests.Repositories;

public class MapRepositoryTests
{
    private readonly MapRepository _target;

    private readonly InMemoryDbContext _dbContext = new();

    public MapRepositoryTests()
    {
        _target = new MapRepository(
            dbContext: _dbContext
        );
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetMapAsync_ShouldBeCorrect(FederalSubject federalSubject, IEnumerable<string> paths)
    {
        // setup
        federalSubject.Map = paths.Select(x => new Map { Path = x }).ToList();

        await _dbContext.FederalSubjects.AddAsync(federalSubject);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _target.GetMapAsync();

        var x = await _dbContext.FederalSubjects.ToListAsync();

        // assert
        result.Should().BeEquivalentTo(
            new[] { GetMapModel(federalSubject) }
        );
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetMapAsync_ShouldBeEmpty(FederalSubject federalSubject)
    {
        // setup
        federalSubject.Map = Enumerable.Empty<Map>().ToList();

        await _dbContext.FederalSubjects.AddAsync(federalSubject);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _target.GetMapAsync();

        // assert
        result.Should().BeEmpty();
    }

    [Theory]
    [NoRecursionInlineAutoData(null)]
    [NoRecursionInlineAutoData("")]
    [NoRecursionInlineAutoData(" ")]
    public async Task GetMapAsync_ShouldBeEmpty_BecausePaths_IsNullOrWhiteSpaces(string path, FederalSubject federalSubject)
    {
        // setup
        federalSubject.Map = Enumerable.Repeat(new Map { Path = path }, 3).ToList();

        await _dbContext.FederalSubjects.AddAsync(federalSubject);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _target.GetMapAsync();

        // assert
        result.Should().BeEmpty();
    }

    private static MapModel GetMapModel(FederalSubject federalSubject) =>
        new()
        {
            FederalSubject = new FederalSubjectModel
            {
                Id = federalSubject.Id,
                FederalDistrictId = federalSubject.FederalDistrictId,
                FederalSubjectTypeId = federalSubject.FederalSubjectTypeId,
                Name = federalSubject.Name,
                Description = federalSubject.Description
            },
            Paths = federalSubject.Map.Select(x => x.Path!)
        };
}
