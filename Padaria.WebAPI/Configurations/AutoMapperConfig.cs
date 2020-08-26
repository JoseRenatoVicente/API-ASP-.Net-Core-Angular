using System;
using AutoMapper;
using Padaria.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Padaria.WebAPI.Configurations
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperProfiles));
        }
    }
}
