using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiigoJournalsController : ControllerBase
    {
        private HttpClient _client;
        string path = "https://api.siigo.com/v1/journals";

        public SiigoJournalsController(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3OTkxNEU2MTJFRkI4NjE5RDNFQ0U4REFGQTU0RDFBMDdCQjM5QjJSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IjE1a1U1aEx2dUdHZFBzNk5yNlZOR2dlN09iSSJ9.eyJuYmYiOjE2NjE0MzgyNjYsImV4cCI6MTY2MTUyNDY2NiwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTAxODMxNSIsImF1dGhfdGltZSI6MTY2MTQzODI2NiwiaWRwIjoibG9jYWwiLCJuYW1lIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJtYWlsX3NpaWdvIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJTaWlnb0FQSSIsInVzZXJzX2lkIjoiNjI5IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDM5MjIwMSIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDA4IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTYyZTNhMTViMTQ4NDg2ZDkyMTYxYjdhZmNiODdmM2MiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiI3OEI1OTE5OEQ4MzNFQTJCQzMyMjZFRUY5NzM4RTE1RCIsImlhdCI6MTY2MTQzODI2Niwic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.QIjwDVOYVCIvhcvhb_BGF5yiJ8rbdKJuaaaLXlvkx0yKvKHovncy38aGlduAty7tdY5MWALJ6XJ8-h1pg07sCzW1WR6pAnjIzmRpmaSPNL9ssTH6DtZWJJVnH8ZdblEKDfCUL5QEfGgZAXq4UUonHB-aC-t3l0VRvDh7bh3RJE8zagaBQtrUimlQ0wbVIcQuMzgak9n6mLPLAQSZayMkwfH2umS5fYvHyXvyj6bt8teS0ELU1pqHmljFBzUnK6qY8_exj4i4ObWgC7x9uS_Ednnergxjf7mOGZXvK42QGatMvWYCL5p9zgRU8KKnNUiDvN6MfVSpW9F1aJuSVikdVw");
        }

        [HttpGet]
        public async Task<string> GetSiigoJournalAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpPost]
        public async Task<string> PostJournalAsync(string data)
        {
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

    }
}
