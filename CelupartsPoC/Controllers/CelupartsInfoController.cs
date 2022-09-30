using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CelupartsInfoController : ControllerBase
    {
        private readonly DataContext _context;

        public CelupartsInfoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CelupartsInfo>>> Get()
        {
            return Ok(await _context.CelupartsInfo.ToListAsync());
        }
    }
}
