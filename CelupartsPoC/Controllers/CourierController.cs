using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly DataContext _context;

        public CourierController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Courier>>> Get()
        {
            return Ok(await _context.Courier.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Courier>> Get(int id)
        {
            var courier = _context.Courier.FindAsync(id);
            if (courier.Result == null)
            {
                return BadRequest("Courier review not found!");
            }
            return Ok(courier.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Courier>>> AddProductReview(Courier courier)
        {
            _context.Courier.Add(courier);
            await _context.SaveChangesAsync();

            return Ok(await _context.Courier.ToListAsync());
        }
    }
}
