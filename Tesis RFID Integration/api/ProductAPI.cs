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

        public async Task<Product> GetProductByEPCAsync(string EPC)
        {
            try
            {
                var response = await _httpClient.GetAsync($"products/epc/{EPC}");

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Product>>(content);

                return apiResponse.Data;
            }
            catch (HttpRequestException e)
            {
                // Manejar aquí los errores de la solicitud
                Console.WriteLine($"Error al realizar la solicitud: {e.Message}");
                return null;
            }
        }

        public async Task<Product> UpdateProductWarehouse(string epc, int warehouseId)
        {
            try
            {
                // Crea el objeto que representa el cuerpo de la solicitud
                var requestBody = new { warehouseId };
                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                System.Diagnostics.Debug.WriteLine($"content: {content}");


                // Realiza la solicitud PATCH
                var response = await _httpClient.PatchAsync($"products/warehouse/{epc}", content);

                System.Diagnostics.Debug.WriteLine($"response: {response}");

                response.EnsureSuccessStatusCode();

                // Deserializa y retorna el producto actualizado
                var responseBody = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine($"responseBody: {responseBody}");

                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<Product>>(responseBody);
                return apiResponse.Data;
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
