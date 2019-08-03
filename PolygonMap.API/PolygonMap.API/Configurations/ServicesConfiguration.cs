using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PolygonMap.Data.Repositories;
using PolygonMap.Domain.Repositories;
using PolygonMap.Domain.Supervisor;
using PolygonMap.Data;
using PolygonMap.Domain.Entities;
using PolygonMap.Domain.ApiModels;

namespace PolygonMap.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPolygonRepository, PolygonRepository>()
                    .AddScoped<IShapeRepository, ShapeRepository>()
                    .AddScoped<IPointRepository, PointRepository>();
                   
            return services;
        }

        public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
        {
            services.AddScoped<IPolygonMapSupervisor, PolygonMapSupervisor>();

            return services;
        }

        public static IServiceCollection ConfigureMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Shape, ShapeApiModel>().ReverseMap();
                cfg.CreateMap<Point, PointApiModel>().ReverseMap();
                cfg.CreateMap<Polygon, PolygonApiModel>().ReverseMap();
               
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        public static IServiceCollection AddConnection(this IServiceCollection services,
            IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("SQLConnection");
            services.AddDbContextPool<PolygonMapContext>(options => options.UseSqlServer(connection, a => a.MigrationsAssembly("PolygonMap.API")));

            return services;
        }
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return services;
        }

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services) =>        
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
                    .Build());
            }
        );
        
        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            services.AddLogging(builder => builder
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Information)
            );

            return services;
        }
    }
}