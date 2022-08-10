using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly DataContext _context;

        public EquipmentController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Equipment>>> Get()
        {
            var equipments = _context.Equipment.ToList();
            return Ok(equipments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> Get(int id)
        {
            var equipment = _context.Equipment.FindAsync(id);
            if (equipment.Result == null)
            {
                return BadRequest("Equipment not found!");
            }
            return Ok(equipment.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Equipment>>> AddEquipment(Equipment equipment)
        {
            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return Ok(await _context.Equipment.FindAsync(equipment.IdEquipment));
        }

        [HttpPut]
        public async Task<ActionResult<List<Equipment>>> UpdateEquipment(Equipment request)
        {
            var dbEquipment = _context.Equipment.FindAsync(request.IdEquipment);
            if(dbEquipment.Result == null)
            {
                return BadRequest("Equipment not found!");
            }
            dbEquipment.Result.TypeOfEquipment = request.TypeOfEquipment;
            dbEquipment.Result.EquipmentBrand = request.EquipmentBrand;
            dbEquipment.Result.ModelOrReference = request.ModelOrReference;
            dbEquipment.Result.Imei = request.Imei;
            dbEquipment.Result.EquipmentInvoice = request.EquipmentInvoice;

            await _context.SaveChangesAsync();

            return Ok(await _context.Equipment.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Equipment>>> Delete(int id)
        {
            var dbEquipment = _context.Equipment.FindAsync(id);
            if (dbEquipment.Result == null)
            {
                return BadRequest("Equipment not found!");
            }
            _context.Equipment.Remove(dbEquipment.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Equipment.ToListAsync());
        }
    }
}
