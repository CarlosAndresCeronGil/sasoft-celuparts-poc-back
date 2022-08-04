using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly DataContext _context;

        public RepairController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Repair>>> Get()
        {
            return Ok(await _context.Repair.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Repair>> Get(int id)
        {
            var repair = _context.Repair.FindAsync(id);
            if (repair.Result == null)
            {
                return BadRequest("Product review not found!");
            }
            return Ok(repair.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Repair>>> AddProductReview(Repair productReview)
        {
            _context.Repair.Add(productReview);
            await _context.SaveChangesAsync();

            return Ok(await _context.Repair.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Repair>>> UpdateProductReview(Repair request)
        {
            var dbProductReview = _context.Repair.FindAsync(request.IdRepair);
            if (dbProductReview.Result == null)
            {
                return BadRequest("Product review not found!");
            }
            dbProductReview.Result.IdRequest = request.IdRequest;
            dbProductReview.Result.IdTechnician = request.IdTechnician;
            dbProductReview.Result.RepairDate = request.RepairDate;
            dbProductReview.Result.DeviceDiagnostic = request.DeviceDiagnostic;
            dbProductReview.Result.RepairQuote = request.RepairQuote;

            await _context.SaveChangesAsync();

            return Ok(await _context.Repair.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Repair>>> Delete(int id)
        {
            var dbProductReview = _context.Repair.FindAsync(id);
            if (dbProductReview.Result == null)
            {
                return BadRequest("Product review not found!");
            }
            _context.Repair.Remove(dbProductReview.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Repair.ToListAsync());
        }
    }
}
