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
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Brand>>> Get()
        {
            return Ok(await _context.Brand.ToListAsync());
        }

        [HttpGet("computers")]
        public async Task<ActionResult<List<Brand>>> GetComputers()
        {
            var result = await _context.Brand.Where(x => x.IdTypeOfEquipment == 1).OrderBy(x => x.BrandName).ToListAsync();
            return Ok(result);
        }

        [HttpGet("cellphones")]
        public async Task<ActionResult<List<Brand>>> GetCellphones()
        {
            var result = await _context.Brand.Where(x => x.IdTypeOfEquipment == 2).OrderBy(x => x.BrandName).ToListAsync();
            return Ok(result);
        }

        [HttpGet("tablets")]
        public async Task<ActionResult<List<Brand>>> GetTablets()
        {
            var result = await _context.Brand.Where(x => x.IdTypeOfEquipment == 3).OrderBy(x => x.BrandName).ToListAsync();
            return Ok(result);
        }

        [HttpGet("smartWatches")]
        public async Task<ActionResult<List<Brand>>> GetSmartWatches()
        {
            var result = await _context.Brand.Where(x => x.IdTypeOfEquipment == 4).OrderBy(x => x.BrandName).ToListAsync();
            return Ok(result);
        }

        [HttpGet("allBrands")]
        public async Task<ActionResult<List<Brand>>> GetAllBrands()
        {
            var result = await _context.Brand.Select(x => x.BrandName).Distinct().ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetById(int id)
        {
            var brand = _context.Brand.FindAsync(id);
            if (brand.Result == null)
            {
                return BadRequest("Brand not found!");
            }
            return Ok(brand.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Brand>>> AddBrand(Brand brand)
        {
            _context.Brand.Add(brand);
            await _context.SaveChangesAsync();

            return Ok(await _context.Brand.FindAsync(brand.IdBrand));
        }

        [HttpPut]
        public async Task<ActionResult<List<Brand>>> UpdateBrand(Brand brand)
        {
            var dbBrand = _context.Brand.FindAsync(brand.IdBrand);
            if(dbBrand.Result == null)
            {
                return BadRequest("Brand not found!");
            }
            dbBrand.Result.IdTypeOfEquipment = brand.IdTypeOfEquipment;
            dbBrand.Result.BrandName = brand.BrandName;

            await _context.SaveChangesAsync();

            return Ok(await _context.Brand.FindAsync(brand.IdBrand));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Brand>>> Delete(int id)
        {
            var dbBrand = _context.Brand.FindAsync(id);
            if(dbBrand.Result == null)
            {
                return BadRequest("Brand not found!");
            }
            _context.Brand.Remove(dbBrand.Result);
            await _context.SaveChangesAsync();

            return Ok(await _context.Brand.ToListAsync());
        }
    }
}
