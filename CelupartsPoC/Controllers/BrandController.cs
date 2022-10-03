using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly DataContext _context;

        public BrandController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Brand>>> Get()
        {
            return Ok(await _context.Brand.ToListAsync());
        }

        /*[HttpGet("{id}")]
        public async Task<ActionResult<Brand>> Get(int id)
        {
            var brand = _context.Brand.FindAsync(id);
            if (brand.Result == null)
            {
                return BadRequest("Brand ");
            }
        }*/
    }
}
