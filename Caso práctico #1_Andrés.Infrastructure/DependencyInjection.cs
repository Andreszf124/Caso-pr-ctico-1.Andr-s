using Caso_práctico__1_Andrés.Domain.Repositorios;
using Caso_práctico__1_Andrés.Infrastructure.Data;
using Caso_práctico__1_Andrés.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caso_práctico__1_Andrés.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ISalaRepository, SalaRepository>();
        services.AddScoped<IReservaRepository, ReservaRepository>();

        return services;
    }
}
