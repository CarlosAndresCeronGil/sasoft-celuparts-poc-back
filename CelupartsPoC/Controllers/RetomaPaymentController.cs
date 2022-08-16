using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetomaPaymentController : ControllerBase
    {
        private readonly DataContext _context;
        public RetomaPaymentController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RetomaPayment>>> Get()
        {
            return Ok(await _context.RetomaPayment.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RetomaPayment>> Get(int id)
        {
            var retomaPayment = _context.RetomaPayment.FindAsync(id);
            if (retomaPayment.Result == null)
            {
                return BadRequest("Retoma payment not found!");
            }
            return Ok(retomaPayment.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<RetomaPayment>>> AddRetomaPayment(RetomaPayment retomaPayment)
        {
            _context.RetomaPayment.Add(retomaPayment);
            await _context.SaveChangesAsync();

            return Ok(await _context.RetomaPayment.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<RetomaPayment>>> UpdateRetomaPayment(RetomaPayment request)
        {
            var dbRetomaPayment = _context.RetomaPayment.FindAsync(request.IdRetomaPayment);
            if (dbRetomaPayment.Result == null)
            {
                return BadRequest("Retoma payment not found!");
            }
            dbRetomaPayment.Result.IdRetoma = request.IdRetoma;
            dbRetomaPayment.Result.PaymentMethod = request.PaymentMethod;
            dbRetomaPayment.Result.PaymentDate = request.PaymentDate;

            await _context.SaveChangesAsync();

            return Ok(await _context.RetomaPayment.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RetomaPayment>>> Delete(int id)
        {
            var dbRetomaPayment = _context.RetomaPayment.FindAsync(id);
            if (dbRetomaPayment.Result == null)
            {
                return BadRequest("Retoma payment not found!");
            }
            _context.RetomaPayment.Remove(dbRetomaPayment.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.RetomaPayment.ToListAsync());
        }
    }
}
