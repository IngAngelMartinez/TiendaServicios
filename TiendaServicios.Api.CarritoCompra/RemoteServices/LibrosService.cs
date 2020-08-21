using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteServices
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<LibrosService> logger;

        public LibrosService(IHttpClientFactory httpClient, ILogger<LibrosService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool IsSucced, RemoteLibro Libro, string Error)> GetLibro(Guid Id)
        {
            try
            {
                var cliente = httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/LibroMaterial/{Id}");

                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<RemoteLibro>(contenido, options);
                    return (true, resultado, null);                
                }

                return (false, null, response.ReasonPhrase);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
