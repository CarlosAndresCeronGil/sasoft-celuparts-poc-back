using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiigoVoucherController : ControllerBase
    {
        private HttpClient _client;
        string path = "https://api.siigo.com/v1/vouchers";

        public SiigoVoucherController(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3OTkxNEU2MTJFRkI4NjE5RDNFQ0U4REFGQTU0RDFBMDdCQjM5QjJSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IjE1a1U1aEx2dUdHZFBzNk5yNlZOR2dlN09iSSJ9.eyJuYmYiOjE2NjExODczMzIsImV4cCI6MTY2MTI3MzczMiwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTAxODMxNSIsImF1dGhfdGltZSI6MTY2MTE4NzMzMiwiaWRwIjoibG9jYWwiLCJuYW1lIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJtYWlsX3NpaWdvIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJTaWlnb0FQSSIsInVzZXJzX2lkIjoiNjI5IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDM5MjIwMSIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDA4IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTYyZTNhMTViMTQ4NDg2ZDkyMTYxYjdhZmNiODdmM2MiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiI1NERDRTc5NkVGNUVBNkU3OUIwOUMxNTQ1NkJGRjMyMSIsImlhdCI6MTY2MTE4NzMzMiwic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.BPIDXF1j1alNe0wUQiHAVllQvc7zqjCUqxVnnFn65SOcOCr8ikJh5hxvACd7sfkAsblMaCg5mywz8l3CiPYcuh_OviEpzSFwFlCBUKoEBi3LdWHlHK46qS2Ez2vMs33H22BhU9Uu9RIKpu8lH4ESQbYOEN2quiqiwNjd7f-P0SfJaGLeO5FLxDdmXfo7eFUjyCrSmaQK-fZCvZKDVFWR6WonnGOh823Nnajdk2lC21gdwn5lPdtIqVjj9yBQLrcsnwfWe3prZMZRHhREgt0re-aVcoEA2faogdrtkiDn39_5lXdBI7cFezkknERAA4Psd08w5-fLPV9f2lueQMH5cg");
        }

        [HttpGet]
        public async Task<string> GetSiigoVoucherAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("{id}")]
        public async Task<string> GetSingleSiigoVoucherAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync(path + $"/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpPost]
        public async Task<string> PostSiigoVoucherAsync(string data)
        {
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpPut("{id}")]
        public async Task<string> PutVoucherAsync(string id, string data)
        {
            HttpResponseMessage response = await _client.PutAsync(path + $"/{id}", new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
