using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NuevoProyectoRESTfulAPI.Conexion;
using NuevoProyectoRESTfulAPI.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using NuevoProyectoRESTfulAPI.ComunicacionSync.Http;

namespace NuevoProyectoRESTfulAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment entorno;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            entorno = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if(entorno.IsProduction()){
                services.AddDbContext<InstitutoDbContext>(op => 
                op.UseSqlServer(Configuration.GetConnectionString("InstitutoProd")));
            }else{
                var servidor = Configuration["DDBBservidor"] ?? "192.168.1.254";
                var puerto = Configuration["DDBBpuerto"] ?? "1433";
                var usuario = Configuration["DDBBusuario"] ?? "ConexionParaAPI";
                var password = Configuration["DDBBpassword"] ?? "123456";
                var ddbb = Configuration["DDBB"] ?? "Instituto X";
                services.AddDbContext<InstitutoDbContext>(op => op.UseSqlServer(
                    $"Server={servidor},{puerto};DataBase={ddbb};User={usuario};Password={password}"
                ));
            }
            services.AddHttpClient<ICampusHistorialCliente, ImplHttpCampusHistorialCliente>();
            services.AddControllers().AddNewtonsoftJson(
                    s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
                );
            services.AddScoped<IEstudianteRepository, ImplEstudianteRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
