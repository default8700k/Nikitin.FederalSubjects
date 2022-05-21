using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Nikitin.FederalSubjects.Database;
using Nikitin.FederalSubjects.Database.Interfaces.Repositories;
using Nikitin.FederalSubjects.Database.Repositories;
using Nikitin.FederalSubjects.WebService.Models;

namespace Nikitin.FederalSubjects.WebService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, ConfigurationModel configuration)
    {
        return services;
    }

    public static IServiceCollection AddNewtonsoftSnakeCaseNamingStrategy(this IServiceCollection services)
    {
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings().UpdateJsonSerializerSettings();
        return services;
    }

    public static IServiceCollection AddNewtonsoftSnakeCaseNamingStrategyInControllers(this IServiceCollection services)
    {
        services.Configure<MvcNewtonsoftJsonOptions>(x => { x.SerializerSettings.UpdateJsonSerializerSettings(); });
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(x =>
            x.UseNpgsql(connectionString)
        );

        services.AddTransient<IMapRepository, MapRepository>();
        return services;
    }

    private static JsonSerializerSettings UpdateJsonSerializerSettings(this JsonSerializerSettings serializerSettings)
    {
        var namingStrategy = new SnakeCaseNamingStrategy();

        serializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = namingStrategy
        };

        serializerSettings.Converters = new JsonConverter[] { new StringEnumConverter(namingStrategy) };
        serializerSettings.Formatting = Formatting.Indented;

        return serializerSettings;
    }
}
