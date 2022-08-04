using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly DataContext _context;

        public TechnicianController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Technician>>> Get()
        {
            return Ok(await _context.Technician.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Technician>> Get(int id)
        {
            var technician = _context.Technician.FindAsync(id);
            if(technician.Result == null)
            {
                return BadRequest("Technician not found!");
            }
            return Ok(technician.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Technician>>> AddTechnician(Technician technician)
        {
            _context.Technician.Add(technician);
            await _context.SaveChangesAsync();

            return Ok(await _context.Technician.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Technician>>> UpdateTechnician(Technician request)
        {
            var dbTechnician = _context.Technician.FindAsync(request.IdTechnician);
            if(dbTechnician.Result == null)
            {
                return BadRequest("Technician not found!");
            }
            dbTechnician.Result.Names = request.Names;
            dbTechnician.Result.Surnames = request.Surnames;
            dbTechnician.Result.Phone = request.Phone;
            dbTechnician.Result.Email = request.Email;
            dbTechnician.Result.Password = request.Password;
            dbTechnician.Result.AccountStatus = request.AccountStatus;

            await _context.SaveChangesAsync();

            return Ok(await _context.Technician.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Technician>>> Delete(int id)
        {
            var dbTechnician = _context.Technician.FindAsync(id);
            if(dbTechnician.Result == null)
            {
                return BadRequest("Technician not found!");
            }
            _context.Technician.Remove(dbTechnician.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Technician.ToListAsync());
        }
    }
}
