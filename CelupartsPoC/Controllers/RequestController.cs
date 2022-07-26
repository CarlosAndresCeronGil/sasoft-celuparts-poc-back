using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        public static List<Request> requests = DataList.requests.ToList();

        [HttpGet]
        public async Task<ActionResult<List<Request>>> Get()
        {
            return Ok(requests);
        }
    }
}
