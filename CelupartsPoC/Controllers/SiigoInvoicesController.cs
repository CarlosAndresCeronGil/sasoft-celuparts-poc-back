using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiigoInvoicesController : ControllerBase
    {
        private HttpClient _client;
        string path = "https://api.siigo.com/v1/invoices";

        public SiigoInvoicesController(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3OTkxNEU2MTJFRkI4NjE5RDNFQ0U4REFGQTU0RDFBMDdCQjM5QjJSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IjE1a1U1aEx2dUdHZFBzNk5yNlZOR2dlN09iSSJ9.eyJuYmYiOjE2NjEyNjQ5NzIsImV4cCI6MTY2MTM1MTM3MiwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTAxODMxNSIsImF1dGhfdGltZSI6MTY2MTI2NDk3MiwiaWRwIjoibG9jYWwiLCJuYW1lIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJtYWlsX3NpaWdvIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJTaWlnb0FQSSIsInVzZXJzX2lkIjoiNjI5IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDM5MjIwMSIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDA4IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTYyZTNhMTViMTQ4NDg2ZDkyMTYxYjdhZmNiODdmM2MiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiI4Njc4MjY5MTZENjYwRUY5QzNGRDVFQjFBMDQxMjAwNSIsImlhdCI6MTY2MTI2NDk3Miwic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.cwsAv3-NhYd84YjYYJbUjbmSHJ6XtTNbO1eqoejOMu5grT2lG8r2G9WY6rxiLQuItLUeQtqqYP2y2cU20Sc4HcE8zrN8b2W1CrQMjFUgurcmI6gqzpDq-T5eA8Rug9zPFxzGMggJM4a4TsdhxmIjfDaigosdZkIcuyvm56KjVVr7LKFUzncuXAVjVsuNj4NKXANNJodBPcZnSBgALQi7iCzWGoFvZSBjeeDELamnECxRyuCR1glOFrEu8nYIhuiZRiUVz2lnSKlyOMMoGIw0r9UDqo5INhDtxtQmrsjy_dOhVDa1kiIFPv9uX1IyTasgANBeIFoOSzTB5UXPsu9awg");
        }

        [HttpGet]
        public async Task<string> GetSiigoInvoicesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("{id}")]
        public async Task<string> GetSingleSiigoInvoiceAsync(string id)
        {
            HttpResponseMessage response = await _client.GetAsync(path + $"/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpPost]
        public async Task<string> PostInvoiceAsync(string data)
        {
            HttpResponseMessage response = await _client.PostAsync(path, new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpPut("{id}")]
        public async Task<string> PutInvoiceAsync(string id, string data)
        {
            HttpResponseMessage response = await _client.PutAsync(path + $"/{id}", new StringContent(data, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpDelete("{id}")]
        public async Task<string> DeleteInvoiceAsync(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync(path + $"/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
