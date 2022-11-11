using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsInfoController : ControllerBase
    {
        private readonly DataContext _context;

        public PartsInfoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PartsInfo>>> Get()
        {
            return Ok(await _context.PartsInfo.ToListAsync());
        }
    }
}
