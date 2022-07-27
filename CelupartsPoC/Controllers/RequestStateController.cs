using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestStateController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestStateController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Equipment>>> Get()
        {
            return Ok(await _context.RequestStates.ToListAsync());
        }
    }
}
