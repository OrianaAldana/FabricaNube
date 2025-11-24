using System.Text;
using System.Text.Json;
using FabricaNube.Dtos;
using FabricaNube.Services;

namespace FabricaNube.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly HttpClient _http;

        public SucursalService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> ActualizarSolicitudAsync(SolicitudActualizarDto dto)
        {
            // URL DEL COMPAÑERO (CAMBIA ESTO)
            string url = "https://https://sucursalsantacruz-production.up.railway.app/swagger/index.html";

            string json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PutAsync(url, content);

            return response.IsSuccessStatusCode;
        }
    }
}

