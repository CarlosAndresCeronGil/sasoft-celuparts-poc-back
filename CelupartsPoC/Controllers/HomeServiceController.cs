using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeServiceController : ControllerBase
    {
        private readonly DataContext _context;

        public HomeServiceController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<HomeService>>> Get()
        {
            return Ok(await _context.HomeService.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HomeService>> Get(int id)
        {
            var homeService = _context.HomeService.FindAsync(id);
            if (homeService.Result == null)
            {
                return BadRequest("Home service not found!");
            }
            return Ok(homeService.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<HomeService>>> AddHomeService(HomeService homeService)
        {
            _context.HomeService.Add(homeService);
            await _context.SaveChangesAsync();

            return Ok(await _context.HomeService.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<HomeService>>> UpdateHomeService(HomeService request)
        {
            var dbHomeService = _context.HomeService.FindAsync(request.IdHomeService);
            if (dbHomeService.Result == null)
            {
                return BadRequest("Home Service not found!");
            }
            dbHomeService.Result.IdRequest = request.IdRequest;
            dbHomeService.Result.IdCourier = request.IdCourier;
            dbHomeService.Result.PickUpDate = request.PickUpDate;
            dbHomeService.Result.DeliveryDate = request.DeliveryDate;

            await _context.SaveChangesAsync();

            return Ok(await _context.HomeService.ToListAsync());
        }

        [HttpPut("byIdRequest")]
        public async Task<ActionResult<List<HomeService>>> UpdateHomeServiceByIdRequest(HomeService request)
        {
            var dbHomeService =  _context.HomeService.Where(x => x.IdRequest == request.IdRequest).FirstOrDefault();
            if(dbHomeService == null)
            {
                return BadRequest("Home service not found!");
            }
            dbHomeService.IdRequest = request.IdRequest;
            dbHomeService.IdCourier = request.IdCourier;
            dbHomeService.DeliveryDate = request.DeliveryDate;

            await _context.SaveChangesAsync();

            return Ok(await _context.HomeService.FindAsync(request.IdRequest));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<HomeService>>> Delete(int id)
        {
            var dbHomeService = _context.HomeService.FindAsync(id);
            if (dbHomeService.Result == null)
            {
                return BadRequest("Home Service not found!");
            }
            _context.HomeService.Remove(dbHomeService.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.HomeService.ToListAsync());
        }
    }
}
