using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Nikitin.FederalSubjects.WebService.Extensions;
using Nikitin.FederalSubjects.WebService.Models;

namespace Nikitin.FederalSubjects.WebService;

public class Startup
{
    private readonly ConfigurationModel _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration.Get<ConfigurationModel>();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

        services.AddControllers().AddNewtonsoftJson();

        services.AddConfiguration(_configuration);

        services.AddNewtonsoftSnakeCaseNamingStrategy();
        services.AddNewtonsoftSnakeCaseNamingStrategyInControllers();

        services.AddDatabase(_configuration.ConnectionStrings.DefaultConnection);

        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerGen(x =>
        {
            x.EnableAnnotations();
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Nikitin.FederalSubjects.WebService", Version = "v1" });
        });

        services.AddHealthChecks();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() == true)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Nikitin.FederalSubjects.WebService v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecks("/health/lite", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                Predicate = (check) => !check.Tags.Contains("deep")
            });
        });
    }
}
