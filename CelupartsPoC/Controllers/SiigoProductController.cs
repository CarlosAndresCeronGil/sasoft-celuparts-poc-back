using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3OTkxNEU2MTJFRkI4NjE5RDNFQ0U4REFGQTU0RDFBMDdCQjM5QjJSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IjE1a1U1aEx2dUdHZFBzNk5yNlZOR2dlN09iSSJ9.eyJuYmYiOjE2NjA5MzYwNTAsImV4cCI6MTY2MTAyMjQ1MCwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTAxODMxNSIsImF1dGhfdGltZSI6MTY2MDkzNjA1MCwiaWRwIjoibG9jYWwiLCJuYW1lIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJtYWlsX3NpaWdvIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJTaWlnb0FQSSIsInVzZXJzX2lkIjoiNjI5IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDM5MjIwMSIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDA4IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTYyZTNhMTViMTQ4NDg2ZDkyMTYxYjdhZmNiODdmM2MiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiJERDgxREFFQjBEOUY4MTFDMjgxQ0JDRkEyQTU2NjU1RiIsImlhdCI6MTY2MDkzNjA1MCwic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.ntZJsBERECm8UEvKqKHzV8aLUzyvPsaCryCztsYbDI88qOgxFn_qdPf9I25GMZK5ZIc_yIXTie_LjF50ynjXT9_taWrmDlaqRBPXzDK9tquDhupkquvAr3ZTEeN8dPLB-ZSDY8NPfM9PDzboU5HhSYrZTm-uSR35xKhk_u4TsrkK7EW4qqy8bbFS0D_s6gVVWLdbqxPw-o6bH4wJ_50-6HlRZ8sy0BXsBVs1Lr-y3eiUImG-ebsFECsRC_Kxs-QFN2qKljxLSNkafg9uqVWK5cMT90RvRUt1b01Rj6rhwlu3pGZ3-lbw7UlsJFSgbOQn_L5rKU54ki-FnoCD3XXNyA");
        }

        /*[HttpGet]
        //public async Task<SiigoProduct> GetSiigoProductsAsync(string path)
        public async Task<List<SiigoProduct>> GetSiigoProductsAsync(string path)
        {
            //SiigoProduct siigoProduct = null;
            var result = new List<SiigoProduct>();

            HttpResponseMessage response = await _client.GetAsync(path);

            var json = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<List<SiigoProduct>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            return result;
        }*/

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

    }
}
