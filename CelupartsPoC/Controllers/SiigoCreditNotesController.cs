using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiigoCreditNotesController : ControllerBase
    {
        private HttpClient _client;
        string path = "https://api.siigo.com/v1/credit-notes";

        public SiigoCreditNotesController(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3OTkxNEU2MTJFRkI4NjE5RDNFQ0U4REFGQTU0RDFBMDdCQjM5QjJSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IjE1a1U1aEx2dUdHZFBzNk5yNlZOR2dlN09iSSJ9.eyJuYmYiOjE2NjQxOTY0NTMsImV4cCI6MTY2NDI4Mjg1MywiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTAxODMxNSIsImF1dGhfdGltZSI6MTY2NDE5NjQ1MywiaWRwIjoibG9jYWwiLCJuYW1lIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJtYWlsX3NpaWdvIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJTaWlnb0FQSSIsInVzZXJzX2lkIjoiNjI5IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDM5MjIwMSIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDA4IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTYyZTNhMTViMTQ4NDg2ZDkyMTYxYjdhZmNiODdmM2MiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiIxMUY2OEI5QjgwRDczNTNCOEZFREM0OUIyQ0Y5OEFCMyIsImlhdCI6MTY2NDE5NjQ1Mywic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.CbP2MPmfBMhvuTGu5o__i7Gi2NZhNAy8e7VFz5iDoYThuijp6mTvXzEmgBSFJJVPls7onqs0ExM_vXKKqy3EL3dZDCxJ_1bLwQ2CUi56iwH6UdG5OmJncFeBglqzobUbQNot2o4qv7mt9XyeMFGO-Tt4T3snolhzj1pOUeqFcGg-TBiDug3CDK6k-7xm6Ie-fBiDyzMCraC4G4dThGSAf6U7xlewFF96ZmGpT4qf3JS2WkU8a2UFIuRqOAHFfW4Cjwi6n7vU-vmUtPawnhagH_2wpVKqnPMTr0284i14_HX3S8PRipheLyZHzvQRByJQsnUBr5sYwO2n4yCCSXDPaQ");
        }

        [HttpGet]
        public async Task<string> GetSiigoCreditNotesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("{id}")]
        public async Task<string> GetSingleSiigoCreditNotesAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync(path + $"/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpPost]
        public async Task<string> PostCreditNotesAsync(string data)
        {
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
