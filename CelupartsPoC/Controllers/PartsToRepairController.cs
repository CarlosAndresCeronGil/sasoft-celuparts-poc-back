using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsToRepairController : ControllerBase
    {
        private readonly DataContext _context;

        public PartsToRepairController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PartsToRepair>>> Get()
        {
            return Ok(await _context.PartsToRepair.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartsToRepair>> GetById(int id)
        {
            var partsToRepair = await _context.PartsToRepair.FindAsync(id);
            if(partsToRepair != null)
            {
                return BadRequest("Part to repair not found!");
            }
            return Ok(partsToRepair);
        }

        [HttpGet("byIdRepair/{idRepair}")]
        public async Task<ActionResult<PartsToRepair>> SearchBydIdRepair(int idRepair)
        {
            var partsToRepair = await _context.PartsToRepair.Where(x => x.IdRepair == idRepair).ToListAsync();
            if(partsToRepair == null)
            {
                return BadRequest("Parts to repair not found!");
            }
            return Ok(partsToRepair);
        }

        [HttpGet("searchRepeated")]
        public async Task<ActionResult<PartsToRepair>> SearchRepeated([FromQuery] int idRepair, [FromQuery] string partName)
        {
            var partsToRepair = await _context.PartsToRepair
                .Where(x => x.IdRepair == idRepair)
                .Where(x => x.Part == partName)
                .ToListAsync();
            if(partsToRepair.Count() == 0)
            {
                return NotFound("Not found");
            }
            return Ok(partsToRepair);
        }

        [HttpPost]
        public async Task<ActionResult<List<PartsToRepair>>> AddPartToRepair(PartsToRepair partsToRepair)
        {
            _context.PartsToRepair.Add(partsToRepair);
            await _context.SaveChangesAsync();

            return Ok(await _context.PartsToRepair.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<PartsToRepair>>> UpdatePartsToRepair(PartsToRepair request)
        {
            var dbPartsToRepair = await _context.PartsToRepair.FindAsync(request.IdPartsToRepair);
            if(dbPartsToRepair == null)
            {
                return BadRequest("Part to repair not found!");
            }
            dbPartsToRepair!.IdPartsToRepair = request.IdPartsToRepair;
            dbPartsToRepair!.Repair = request.Repair;
            dbPartsToRepair.Part = request.Part;
            dbPartsToRepair.ToReplace = request.ToReplace;
            dbPartsToRepair.ToRepair = request.ToRepair;

            await _context.SaveChangesAsync();

            return Ok(await _context.PartsToRepair.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PartsToRepair>>> Delete(int id)
        {
            var dbPartsToRepair = await _context.PartsToRepair.FindAsync(id);
            if(dbPartsToRepair == null)
            {
                return BadRequest("Part to repair not found!");
            }
            _context.PartsToRepair.Remove(dbPartsToRepair);
            await _context.SaveChangesAsync();

            return Ok(await _context.PartsToRepair.ToListAsync());
        }

        [HttpDelete("byIdRequestAndPart")]
        public async Task<ActionResult<List<PartsToRepair>>> DeleteByIdRequestAndPart([FromQuery] int idRepair, [FromQuery] string partName)
        {
            var partsToRepair = _context.PartsToRepair
                .Where(x => x.IdRepair == idRepair)
                .Where(x => x.Part == partName)
                .FirstOrDefault();
            if(partsToRepair == null)
            {
                return BadRequest("Parts to repair not found!");
            }
            _context.PartsToRepair.Remove(partsToRepair);
            await _context.SaveChangesAsync();

            return Ok(await _context.PartsToRepair.ToListAsync());
        }
    }
}
