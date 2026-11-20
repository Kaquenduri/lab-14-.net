using lab10.Application.Repositories;
using lab10.Infrastructure.Persistence;
using lab10.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace lab10.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //DataBase Connection
        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });

        //ServicesRegister
        services.AddScoped<IResponseRepository, ResponseRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITIcketRepository, TIcketRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        return services;
    }
}