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
            var repairs = _context.Repair.Include(x => x.Technician);
            return Ok(repairs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Repair>> Get(int id)
        {
            var repair = _context.Repair.Include(x => x.PartsToRepair).Where(x=> x.IdRepair == id);
            if (repair == null)
            {
                return BadRequest("Repair not found!");
            }
            return Ok(repair);
        }

        [HttpPost]
        public async Task<ActionResult<List<Repair>>> AddProductReview(Repair repair)
        {
            _context.Repair.Add(repair);
            await _context.SaveChangesAsync();

            return Ok(await _context.Repair.FindAsync(repair.IdRepair));
        }

        [HttpPut]
        public async Task<ActionResult<List<Repair>>> UpdateRepair(Repair request)
        {
            var dbRepair = _context.Repair.FindAsync(request.IdRepair);
            if (dbRepair.Result == null)
            {
                return BadRequest("Repair not found!");
            }
            if(request.RepairStartDate != null && request.RepairDate != null)
            {
                dbRepair.Result.IdRequest = request.IdRequest;
                dbRepair.Result.IdTechnician = request.IdTechnician;
                dbRepair.Result.RepairStartDate = request.RepairStartDate;
                dbRepair.Result.RepairDate = request.RepairDate;
                dbRepair.Result.DeviceDiagnostic = request.DeviceDiagnostic;
                dbRepair.Result.RepairQuote = request.RepairQuote;
                dbRepair.Result.RepairTime = request.RepairDate - request.RepairStartDate;
                dbRepair.Result.PriceReviewedByAdmin = request.PriceReviewedByAdmin;
            } else
            {
                dbRepair.Result.IdRequest = request.IdRequest;
                dbRepair.Result.IdTechnician = request.IdTechnician;
                dbRepair.Result.RepairStartDate = request.RepairStartDate;
                dbRepair.Result.RepairDate = request.RepairDate;
                dbRepair.Result.DeviceDiagnostic = request.DeviceDiagnostic;
                dbRepair.Result.RepairQuote = request.RepairQuote;
                dbRepair.Result.PriceReviewedByAdmin = request.PriceReviewedByAdmin;
            }

            await _context.SaveChangesAsync();

            return Ok(await _context.Repair.ToListAsync());
        }

        [HttpPut("repairStartDate/byIdRequest/{id}")]
        public async Task<ActionResult<List<Repair>>> UpdateRepairStartDateByIdRepair(int id)
        {
            var dbRepair = _context.Repair.Where(x => x.IdRequest == id).FirstOrDefault();
            if(dbRepair == null)
            {
                return BadRequest("Repair Not Found");
            }
            dbRepair.RepairStartDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(dbRepair);
        }
        
        [HttpPut("repairTecnhnician/byIdRequest/{id}/{idTechnician}")]
        public async Task<ActionResult<List<Repair>>> UpdateRepairTechnicianByIdRepair(int id, int idTechnician)
        {
            var dbRepair = _context.Repair.Where(x => x.IdRequest == id).FirstOrDefault();
            if (dbRepair == null)
            {
                return BadRequest("Repair Not Found");
            }
            dbRepair.IdTechnician = idTechnician;

            await _context.SaveChangesAsync();

            return Ok(dbRepair);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Repair>>> Delete(int id)
        {
            var dbRepair = _context.Repair.FindAsync(id);
            if (dbRepair.Result == null)
            {
                return BadRequest("Repair not found!");
            }
            _context.Repair.Remove(dbRepair.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Repair.ToListAsync());
        }
    }
}
