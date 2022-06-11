using FluentAssertions;
using Newtonsoft.Json;
using Nikitin.FederalSubjects.Database.Models;
using Nikitin.FederalSubjects.Database.Models.DbModels;
using Testing.Shared;
using Xunit;

namespace Nikitin.FederalSubjects.WebService.Tests.Controllers;

public class MapControllerIntegrationTests : IClassFixture<WebServiceTestFactory>
{
    private readonly WebServiceTestFactory _fixture;

    public MapControllerIntegrationTests(WebServiceTestFactory fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [NoRecursionAutoData]
    public async Task GetMapAsync_ShouldBeCorrect(IEnumerable<FederalSubject> federalSubjects)
    {
        // setup
        var dbContext = _fixture.GetDbContext();

        await dbContext.FederalSubjects.AddRangeAsync(federalSubjects);
        await dbContext.SaveChangesAsync();

        // act
        var result = await _fixture.HttpClient.GetAsync("/map");

        // assert
        result.Should().Be200Ok();
        result.Content.Headers.ContentType.Should().BeEquivalentTo(
            new
            {
                CharSet = "utf-8",
                MediaType = "application/json"
            }
        );

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
