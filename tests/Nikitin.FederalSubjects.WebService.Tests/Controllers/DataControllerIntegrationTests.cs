using FluentAssertions;
using Newtonsoft.Json;
using Nikitin.FederalSubjects.Database.Models;
using Nikitin.FederalSubjects.Database.Models.DbModels;
using Testing.Shared;
using Xunit;

namespace Nikitin.FederalSubjects.WebService.Tests.Controllers;

public class DataControllerIntegrationTests : IClassFixture<WebServiceTestFactory>
{
    private readonly WebServiceTestFactory _fixture;

    public DataControllerIntegrationTests(WebServiceTestFactory fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetFederalDistrictsAsync_ShouldBeCorrect(IEnumerable<FederalDistrict> federalDistricts)
    {
        // setup
        var dbContext = _fixture.GetDbContext(ensureDeleted: true);

        await dbContext.FederalDistricts.AddRangeAsync(federalDistricts);
        await dbContext.SaveChangesAsync();

        // act
        var result = await _fixture.HttpClient.GetAsync("/federal_districts");

        // assert
        ValidateResponse(result);

        var content = await result.Content.ReadAsStringAsync();
        content.Should().Be(
            JsonConvert.SerializeObject(
                new
                {
                    federal_districts = federalDistricts.Select(x =>
                        GetFederalDistrictModel(x)
                    )
                }
            )
        );
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetFederalSubjectTypesAsync_ShouldBeCorrect(IEnumerable<FederalSubjectType> federalSubjectTypes)
    {
        // setup
        var dbContext = _fixture.GetDbContext(ensureDeleted: true);

        await dbContext.FederalSubjectTypes.AddRangeAsync(federalSubjectTypes);
        await dbContext.SaveChangesAsync();

        // act
        var result = await _fixture.HttpClient.GetAsync("/federal_subject_types");

        // assert
        ValidateResponse(result);

        var content = await result.Content.ReadAsStringAsync();
        content.Should().Be(
            JsonConvert.SerializeObject(
                new
                {
                    federal_subject_types = federalSubjectTypes.Select(x =>
                        GetFederalSubjectTypeModel(x)
                    )
                }
            )
        );
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetMapAsync_ShouldBeCorrect(IEnumerable<FederalSubject> federalSubjects)
    {
        // setup
        var dbContext = _fixture.GetDbContext(ensureDeleted: true);

        await dbContext.FederalSubjects.AddRangeAsync(federalSubjects);
        await dbContext.SaveChangesAsync();

        // act
        var result = await _fixture.HttpClient.GetAsync("/map");

        // assert
        ValidateResponse(result);

        var content = await result.Content.ReadAsStringAsync();
        content.Should().Be(
            JsonConvert.SerializeObject(
                new
                {
                    map = federalSubjects.Select(x =>
                        GetMapModel(x)
                    )
                }
            )
        );
    }

    private static void ValidateResponse(HttpResponseMessage response)
    {
        response.Should().Be200Ok();
        response.Content.Headers.ContentType.Should().BeEquivalentTo(
            new
            {
                CharSet = "utf-8",
                MediaType = "application/json"
            }
        );
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

    private static FederalDistrictModel GetFederalDistrictModel(FederalDistrict federalDistrict) =>
        new()
        {
            Id = federalDistrict.Id,
            Name = federalDistrict.Name
        };

    private static FederalSubjectTypeModel GetFederalSubjectTypeModel(FederalSubjectType federalSubjectType) =>
        new()
        {
            Id = federalSubjectType.Id,
            Name = federalSubjectType.Name
        };
}
