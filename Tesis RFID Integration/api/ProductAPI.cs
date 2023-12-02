using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesis_RFID_Integration.models;

namespace Tesis_RFID_Integration.api
{
    internal class ProductAPI
    {
        private readonly HttpClient _httpClient;

        public ProductAPI()
        {
            _httpClient = new HttpClient();
            // Configura aquí la URL base si es constante
            _httpClient.BaseAddress = new Uri("http://localhost:3000/api/");
        }

        public async Task<List<Product>> GetProductByEPCAsync(string EPC)
        {
            try
            {
                var response = await _httpClient.GetAsync("products/epc/:epc");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(content);

                return products;
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
