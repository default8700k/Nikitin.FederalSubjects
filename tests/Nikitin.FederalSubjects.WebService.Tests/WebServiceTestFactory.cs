using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nikitin.FederalSubjects.Database;

namespace Nikitin.FederalSubjects.WebService.Tests;

public class WebServiceTestFactory : WebApplicationFactory<Startup>
{
    public HttpClient HttpClient { get; }

    private readonly string _databaseName = Guid.NewGuid().ToString();

    public WebServiceTestFactory()
    {
        HttpClient = base.CreateClient();
    }

    public AppDbContext GetDbContext() =>
        new(
            options: new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(_databaseName)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options
        );

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(
            services =>
            {
                services.RemoveAll<DbContextOptions<AppDbContext>>();
                services.AddDbContext<AppDbContext>(options => options
                    .UseInMemoryDatabase(_databaseName)
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                );
            }
        );
    }
}
