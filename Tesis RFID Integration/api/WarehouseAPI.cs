using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesis_RFID_Integration.models;
using Newtonsoft.Json;

namespace Tesis_RFID_Integration.api
{
    internal class WarehouseAPI
    {
        private readonly HttpClient _httpClient;

        public WarehouseAPI()
        {
            _httpClient = new HttpClient();
            // Configura aquí la URL base si es constante
            _httpClient.BaseAddress = new Uri("http://localhost:3000/api/");
        }

        public async Task<List<Warehouse>> GetWarehousesNamesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("warehouses/names");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var warehouses = JsonConvert.DeserializeObject<List<Warehouse>>(content);

                return warehouses;
            }
            catch (HttpRequestException e)
            {
                // Manejar aquí los errores de la solicitud
                Console.WriteLine($"Error al realizar la solicitud: {e.Message}");
                return null;
            }
        }
    }
}
