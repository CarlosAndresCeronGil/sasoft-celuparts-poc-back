using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairPaymentController : ControllerBase
    {
        private readonly DataContext _context;

        public RepairPaymentController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RepairPayment>>> Get()
        {
            return Ok(await _context.RepairPayment.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RepairPayment>> Get(int id)
        {
            var repairPayment = _context.RepairPayment.FindAsync(id);
            if (repairPayment.Result == null)
            {
                return BadRequest("Repair payment not found!");
            }
            return Ok(repairPayment.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<RepairPayment>>> AddRepairPayment(RepairPayment repairPayment)
        {
            _context.RepairPayment.Add(repairPayment);
            await _context.SaveChangesAsync();

            return Ok(await _context.RepairPayment.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<RepairPayment>>> UpdateRepairPayment(RepairPayment request)
        {
            var dbRepairPayment = _context.RepairPayment.FindAsync(request.IdRepairPayment);
            if (dbRepairPayment.Result == null)
            {
                return BadRequest("Repair payment not found!");
            }
            dbRepairPayment.Result.IdRepair = request.IdRepair;
            dbRepairPayment.Result.PaymentMethod = request.PaymentMethod;
            dbRepairPayment.Result.BillPayment = request.BillPayment;
            dbRepairPayment.Result.PaymentDate = request.PaymentDate;

            await _context.SaveChangesAsync();

            return Ok(await _context.RepairPayment.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RepairPayment>>> Delete(int id)
        {
            var dbRepairPayment = _context.RepairPayment.FindAsync(id);
            if (dbRepairPayment.Result == null)
            {
                return BadRequest("Repair payment not found!");
            }
            _context.RepairPayment.Remove(dbRepairPayment.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.RepairPayment.ToListAsync());
        }
    }
}
