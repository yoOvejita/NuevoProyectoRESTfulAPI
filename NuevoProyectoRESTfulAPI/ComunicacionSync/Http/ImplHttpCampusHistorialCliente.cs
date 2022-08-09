using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NuevoProyectoRESTfulAPI.DTO;

namespace NuevoProyectoRESTfulAPI.ComunicacionSync.Http
{
    public class ImplHttpCampusHistorialCliente : ICampusHistorialCliente
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        public ImplHttpCampusHistorialCliente(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task ComunicarseConCampus(EstudianteReadDTO est)
        {
            StringContent cuerpoHttp = new StringContent(JsonSerializer.Serialize(est), Encoding.UTF8, "application/json");
            var respuesta = await httpClient.PostAsync($"{configuration["CampusService"]}/api/Historial", cuerpoHttp);
            if (respuesta.IsSuccessStatusCode)
                Console.WriteLine("Envío de POST sincronizado, hacia el servicio, Campus tuvo éxito");
            else
                Console.WriteLine("Envío de POST sincronizado, hacia el servicio, Campus NO tuvo éxito");
        }
    }
}

