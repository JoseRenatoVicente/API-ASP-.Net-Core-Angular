using Microsoft.Extensions.DependencyInjection;
using Padaria.Application.Data;
using Padaria.Infra.Data.Repository;
using System;

namespace Padaria.WebAPI.Configurations
{
    public static class RepositoriesConfig
    {
        public static void AddRepositoriesConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<SeedingService>();
            services.AddScoped<IPadariaRepository, PadariaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
