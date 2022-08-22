using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CelupartsPoC.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class SiigoCustomerController : ControllerBase
        {
            private HttpClient _client;
            string path = "https://api.siigo.com/v1/customers";

            public SiigoCustomerController(HttpClient client)
            {
                _client = client;
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3OTkxNEU2MTJFRkI4NjE5RDNFQ0U4REFGQTU0RDFBMDdCQjM5QjJSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IjE1a1U1aEx2dUdHZFBzNk5yNlZOR2dlN09iSSJ9.eyJuYmYiOjE2NjExMDA0OTQsImV4cCI6MTY2MTE4Njg5NCwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTAxODMxNSIsImF1dGhfdGltZSI6MTY2MTEwMDQ5NCwiaWRwIjoibG9jYWwiLCJuYW1lIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJtYWlsX3NpaWdvIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJTaWlnb0FQSSIsInVzZXJzX2lkIjoiNjI5IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDM5MjIwMSIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDA4IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTYyZTNhMTViMTQ4NDg2ZDkyMTYxYjdhZmNiODdmM2MiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiIwQTQ1RTgxRTA0RDU3NDcxNjFBRjAxNUVGREU2QUFBMyIsImlhdCI6MTY2MTEwMDQ5NCwic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.Z7zHo5oefLmnDFPz6dFV7Hn7zZU-kURC4gHRyLRAQTyZ-ARTjNwY7uCeTRYqfqUdqADJHTlRj4XSwqSFMn_fVdspvcOdnypu_xagOCv0ALXAeqICvC8LBNl6kbcXeavEfjTy0r6TJJ-nhjCbL3gV_vxsFNq29HdudBsX8xJs7MqEpQ9MJGuRDHT1YatX_-hkNPmKsunqdv9qLYN2MOT8AenwRG2DIwSrzppgLFTskUGxYe8DPUFIUxbB5AbSlf85a7IxZEqE8Nx9JvduRvivgc3akypwC7RmJ_bMlI_xTMd7Ltfd7G3I3uYO7ewsvsGdpeTpPe92C8NLGCjAYzCjIQ");
            }

            [HttpGet]
            public async Task<string> GetSiigoCustomerAsync()
            {
                HttpResponseMessage response = await _client.GetAsync(path);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return json;
            }

            [HttpGet("{id}")]
            public async Task<string> GetSingleSiigoCustomerAsync(string id)
            {
                HttpResponseMessage response = await _client.GetAsync(path + $"/{id}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return json;
            }

            [HttpPost]
            public async Task<string> PostCustomerAsync(string data)
            {
                HttpResponseMessage response = await _client.PostAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return json;
            }

            [HttpPut("{id}")]
            public async Task<string> PutCustomerAsync(string id, string data)
            {
                HttpResponseMessage response = await _client.PutAsync(path + $"/{id}", new StringContent(data, Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return json;
            }

            [HttpDelete("{id}")]
            public async Task<string> DeleteCustomerAsync(string id)
            {
                HttpResponseMessage response = await _client.DeleteAsync(path + $"/{id}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return json;
            }

        }
}



