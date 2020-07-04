using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Padaria.WebAPI.Data;
using Padaria.Repository;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Padaria.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            /* services.AddControllers();           
             services.AddDbContext<DataContext>(
                 x => x.UseSqlServer(Configuration.GetConnectionString("DessfaultConnection")));
              */

            services.AddScoped<IPadariaRepository, PadariaRepository>();

            services.AddDbContext<DataContext>(options =>
                   options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), builder =>
                    builder.MigrationsAssembly("Padaria.Repository")));//NOME DO PROJETO

            //services.AddAutoMapper(); nao funciona
            services.AddAutoMapper(typeof(Startup));
            /*
                   services.AddDbContext<DataContext>(options =>
                   options.UseMySql(Configuration.GetConnectionString("DataContext")));//NOME DO PROJETO
            */
            services.AddScoped<SeedingService>();
                    services.AddCors();//para permitir conexões cruzadas
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
        {
            if (env.IsDevelopment())
            {
                //mostra a tela de erro no Asp .Net
                app.UseDeveloperExceptionPage();
                //Inicia o seedservice para popular o banco de dados
                seedingService.Seed();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseStaticFiles();//Permite arquivos estaticos como imagens
            app.UseStaticFiles( new StaticFileOptions() { 
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
