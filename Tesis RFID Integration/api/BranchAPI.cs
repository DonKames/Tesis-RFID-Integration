using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesis_RFID_Integration.models;

namespace Tesis_RFID_Integration.api
{
    internal class BranchAPI
    {
        private readonly HttpClient _httpClient;

        public BranchAPI()
        {
            _httpClient = new HttpClient();
            // Configura aquí la URL base si es constante
            _httpClient.BaseAddress = new Uri("http://localhost:3000/api/");
        }

        public async Task<List<Branches>> GetBranchNamesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("branches/names");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var branches = JsonConvert.DeserializeObject<List<Branches>>(content);

                return branches;
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
