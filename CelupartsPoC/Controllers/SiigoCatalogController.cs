using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SiigoCatalogController : ControllerBase
    {
        private HttpClient _client;
        string pathAccountGroups = "https://api.siigo.com/v1/account-groups";
        string pathTaxes = "https://api.siigo.com/v1/taxes";
        string pathPriceList = "https://api.siigo.com/v1/price-lists";
        string pathWareHouses = "https://api.siigo.com/v1/warehouses";
        string pathUsers = "https://api.siigo.com/v1/users";
        string pathDocumentTypes = "https://api.siigo.com/v1/document-types?type=FV";
        string pathPaymentTypes = "https://api.siigo.com/v1/payment-types?document_type=FV";
        string pathCostCenters = "https://api.siigo.com/v1/cost-centers";
        string pathFixedAssets = "https://api.siigo.com/v1/fixed-assets";

        public SiigoCatalogController(HttpClient client)
        {
            _client = client;
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ3OTkxNEU2MTJFRkI4NjE5RDNFQ0U4REFGQTU0RDFBMDdCQjM5QjJSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IjE1a1U1aEx2dUdHZFBzNk5yNlZOR2dlN09iSSJ9.eyJuYmYiOjE2NjM3MDc5NjMsImV4cCI6MTY2Mzc5NDM2MywiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwIiwiYXVkIjoiaHR0cDovL21zLXNlY3VyaXR5c2VydmljZTo1MDAwL3Jlc291cmNlcyIsImNsaWVudF9pZCI6IlNpaWdvQVBJIiwic3ViIjoiMTAxODMxNSIsImF1dGhfdGltZSI6MTY2MzcwNzk2MywiaWRwIjoibG9jYWwiLCJuYW1lIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJtYWlsX3NpaWdvIjoic2lpZ29hcGlAcHJ1ZWJhcy5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJTaWlnb0FQSSIsInVzZXJzX2lkIjoiNjI5IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDM5MjIwMSIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiNDA4IiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTYyZTNhMTViMTQ4NDg2ZDkyMTYxYjdhZmNiODdmM2MiLCJhY2NvdW50YW50IjoiZmFsc2UiLCJqdGkiOiI3NUU2NDNFNkMzMDhGNTRFMUFEMEIzODFGREQwMjA2NSIsImlhdCI6MTY2MzcwNzk2Mywic2NvcGUiOlsiU2lpZ29BUEkiXSwiYW1yIjpbImN1c3RvbSJdfQ.ZwTjhdwRdwMsRgGmDuRaUCNtohWVVuwMnMBNn5pD3EMA2eXQgnEIbMHEaG15yArLZcqeIElU_l6HRVZILYhTL5m83J49XaIK_Jh25SGWSYIeS63LSvab89K5XGKsrJ39VnlrZLdI2qYAS-W9zm36u-3Fix8D6bnfVQZkPaxd6ujeXKBQsUk0-vh1WrsNmw4YmN4W5Qnx78-B_snKm31kgNhyatb14UNwDWBHUCGGZvyGRmXRdd4lhQB9eszlgcG4lBumjh-mQ86Vf0sJWbE-GHhy8ZVIrWFOfZVKgsXw8jRWya7sC27WPq8Dvyrw508ES_jnCv-L-8lpn_SS93QoxA");
        }

        [HttpGet("AccountGroups")]
        public async Task<string> GetSiigoAccountGroupsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathAccountGroups);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("Taxes")]
        public async Task<string> GetSiigoTaxesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathTaxes);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("PriceList")]
        public async Task<string> GetSiigoPriceListAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathPriceList);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("WareHouses")]
        public async Task<string> GetSiigoWareHousesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathWareHouses);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("Users")]
        public async Task<string> GetSiigoUsersAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathUsers);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("DocumentTypes")]
        public async Task<string> GetSiigoDocumentTypesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathDocumentTypes);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("PaymentTypes")]
        public async Task<string> GetSiigoPaymentTypesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathPaymentTypes);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("CostCenters")]
        public async Task<string> GetSiigoCostCentersTypesAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathCostCenters);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [HttpGet("FixedAssets")]
        public async Task<string> GetSiigoFixedAssetsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync(pathFixedAssets);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }

    }
}
