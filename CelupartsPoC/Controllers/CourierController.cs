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

        [HttpPut]
        public async Task<ActionResult<List<Courier>>> UpdateTechnician(Courier request)
        {
            var dbCourier = _context.Courier.FindAsync(request.IdCourier);
            if (dbCourier.Result == null)
            {
                return BadRequest("Courier not found!");
            }
            dbCourier.Result.Names = request.Names;
            dbCourier.Result.Surnames = request.Surnames;
            dbCourier.Result.Phone = request.Phone;
            dbCourier.Result.Email = request.Email;
            dbCourier.Result.Password = request.Password;
            dbCourier.Result.AccountStatus = request.AccountStatus;

            await _context.SaveChangesAsync();

            return Ok(await _context.Courier.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<Courier>>> Delete(int id)
        {
            var dbCourier = _context.Courier.FindAsync(id);
            if (dbCourier.Result == null)
            {
                return BadRequest("Courier not found!");
            }
            _context.Courier.Remove(dbCourier.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Technician.ToListAsync());
        }
    }
}
