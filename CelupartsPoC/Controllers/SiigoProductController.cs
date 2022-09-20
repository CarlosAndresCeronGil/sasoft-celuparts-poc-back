using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiigoProductController : ControllerBase
    {
        private HttpClient _client;
        string path = "https://api.siigo.com/v1/products";

        public SiigoProductController(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3OTkxNEU2MTJFRkI4NjE5RDNFQ0U4REFGQTU0RDFBMDdCQjM5QjJSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IjE1a1U1aEx2dUdHZFBzNk5yNlZOR2dlN09iSSJ9.eyJuYmYiOjE2NjM3MDc5NjMsImV4cCI6MTY2Mzc5NDM2MywiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTAxODMxNSIsImF1dGhfdGltZSI6MTY2MzcwNzk2MywiaWRwIjoibG9jYWwiLCJuYW1lIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJtYWlsX3NpaWdvIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJTaWlnb0FQSSIsInVzZXJzX2lkIjoiNjI5IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDM5MjIwMSIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDA4IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTYyZTNhMTViMTQ4NDg2ZDkyMTYxYjdhZmNiODdmM2MiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiI3NUU2NDNFNkMzMDhGNTRFMUFEMEIzODFGREQwMjA2NSIsImlhdCI6MTY2MzcwNzk2Mywic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.ZwTjhdwRdwMsRgGmDuRaUCNtohWVVuwMnMBNn5pD3EMA2eXQgnEIbMHEaG15yArLZcqeIElU_l6HRVZILYhTL5m83J49XaIK_Jh25SGWSYIeS63LSvab89K5XGKsrJ39VnlrZLdI2qYAS-W9zm36u-3Fix8D6bnfVQZkPaxd6ujeXKBQsUk0-vh1WrsNmw4YmN4W5Qnx78-B_snKm31kgNhyatb14UNwDWBHUCGGZvyGRmXRdd4lhQB9eszlgcG4lBumjh-mQ86Vf0sJWbE-GHhy8ZVIrWFOfZVKgsXw8jRWya7sC27WPq8Dvyrw508ES_jnCv-L-8lpn_SS93QoxA");
        }

        [HttpGet]
        public async Task<string> GetSiigoProductAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("{id}")]
        public async Task<string> GetSingleSiigoProductAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync(path + $"/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpPost]
        public async Task<string> PostProductAsync([FromBody] object data)
        {
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(data.ToString(), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        /*[HttpPost]
        public async Task<string> PostProductAsync(SiigoProduct data)
        {
            var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(path, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }*/

        [HttpPut("{id}")]
        public async Task<string> PutProductAsync(string id, string data)
        {
            HttpResponseMessage response = await _client.PutAsync(path + $"/{id}", new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(path + $"/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

    }
}
