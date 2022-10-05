using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOfEquipmentController : ControllerBase
    {
        private readonly DataContext _context;

        public TypeOfEquipmentController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<TypeOfEquipment>>> Get()
        {
            var typeOfEquipments = _context.TypeOfEquipment.ToListAsync();
            return Ok(typeOfEquipments.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetById(int id)
        {
            var typeOfEquipment = _context.TypeOfEquipment.FindAsync(id);
            if(typeOfEquipment.Result == null)
            {
                return BadRequest("Type of equipment not found!");
            }
            return Ok(typeOfEquipment.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<TypeOfEquipment>>> AddTypeOfEquipment(TypeOfEquipment typeOfEquipment)
        {
            _context.TypeOfEquipment.Add(typeOfEquipment);
            await _context.SaveChangesAsync();

            return Ok(await _context.TypeOfEquipment.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<TypeOfEquipment>>> UpdateTypeOfEquipment(TypeOfEquipment typeOfEquipment)
        {
            var dbTypeOfEquipment = _context.TypeOfEquipment.FindAsync(typeOfEquipment.IdTypeOfEquipment);
            if(dbTypeOfEquipment.Result == null)
            {
                return BadRequest("Type of equipment not found");
            }
            dbTypeOfEquipment.Result.EquipmentTypeName = typeOfEquipment.EquipmentTypeName;

            await _context.SaveChangesAsync();

            return Ok(await _context.TypeOfEquipment.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<TypeOfEquipment>>> Delete(int id)
        {
            var dbTypeOfEquipment = _context.TypeOfEquipment.FindAsync(id);
            if (dbTypeOfEquipment.Result == null)
            {
                return BadRequest("Type of equipment not found!");
            }
            _context.TypeOfEquipment.Remove(dbTypeOfEquipment.Result);
            await _context.SaveChangesAsync();

            return Ok(await _context.TypeOfEquipment.ToListAsync());
        }
    }
}
