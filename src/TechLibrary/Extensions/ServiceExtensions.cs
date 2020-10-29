using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechLibrary.Config;
using TechLibrary.Interfaces;

namespace TechLibrary.Extensions
{
    public static class ServiceExtensions
    {


        internal static IServiceCollection AddLoggerFactory(this IServiceCollection serviceCollection)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
#if DEBUG
                    .AddFilter("Microsoft", LogLevel.Debug)
                    .AddFilter("Application", LogLevel.Debug)
                    .AddFilter("System", LogLevel.Debug)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
#else
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("Application", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Warning)
#endif
                    .AddConsole()
                    .AddEventLog();

            });
            
            serviceCollection.AddSingleton<ILoggerFactory>(loggerFactory);
            return serviceCollection;
        }

        internal static IServiceCollection AddLogger<TLoggerConext>(this IServiceCollection serviceCollection)
        {
           serviceCollection.AddSingleton<ILogger<TLoggerConext>>(sp =>
           {
               ILoggerFactory factory = sp.GetRequiredService<ILoggerFactory>();

               return factory.CreateLogger<TLoggerConext>();
           });
            return serviceCollection;
        }

        public static IServiceCollection AddAppConfig(
                this IServiceCollection services, IConfiguration config)
        {

            services.AddSingleton(config);
            services.AddSingleton<IAppConfig, AppConfig>();

            return services;
        }

        internal static IServiceCollection AddAutoMapperConfig(this IServiceCollection serviceCollection)
        {
            MapperConfiguration mapperConfiguration;
            IMapper mapper;

            mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddMaps(
                    "TechLibrary"
                )
            );

            mapperConfiguration.CompileMappings();
            mapper = mapperConfiguration.CreateMapper();

            serviceCollection.AddSingleton(mapper);

            return serviceCollection;
        }

    }
}
