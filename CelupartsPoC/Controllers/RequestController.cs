using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Request>>> Get()
        {
            var requestWithEquipments = _context.Request.Select(request => new RequestWithEquipments()
            {
                IdRequest = request.IdRequest,
                IdUser = request.IdUser,
                RequestType = request.RequestType,
                PickUpAddress = request.PickUpAddress,
                DeliveryAddress = request.DeliveryAddress,
                PickUpTime = request.PickUpTime,
                PaymentMethod = request.PaymentMethod,
                Quote = request.Quote,
                StatusQuote = request.StatusQuote,
                Equipments = request.Equipments.Select(n => n).ToList(),
                RequestStatus = request.RequestStatus.Select(n => n).ToList(),
            }).ToList();

            return Ok(await _context.Request.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> Get(int id)
        {
            var request = _context.Request.FindAsync(id);
            if(request.Result == null)
            {
                return BadRequest("Request not found!");
            }
            return Ok(request.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Request>>> AddUser(Request request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return Ok(await _context.Request.FindAsync(request.IdRequest));
        }

        [HttpPut]
        public async Task<ActionResult<List<Request>>> UpdateRequest(Request requestR)
        {
            var dbRequest = _context.Request.FindAsync(requestR.IdRequest);
            if (dbRequest.Result == null)
            {
                return BadRequest("Request not found!");
            }
            dbRequest.Result.IdUser = requestR.IdUser;
            dbRequest.Result.RequestType = requestR.RequestType;
            dbRequest.Result.PickUpAddress = requestR.PickUpAddress;
            dbRequest.Result.DeliveryAddress = requestR.DeliveryAddress;
            dbRequest.Result.PickUpTime = requestR.PickUpTime;
            dbRequest.Result.PaymentMethod = requestR.PaymentMethod;
            dbRequest.Result.Quote = requestR.Quote;
            dbRequest.Result.StatusQuote = requestR.StatusQuote;

            await _context.SaveChangesAsync();

            return Ok(await _context.Request.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Request>>> Delete(int id)
        {
            var dbRequest = _context.Request.FindAsync(id);
            if (dbRequest.Result == null)
            {
                return BadRequest("Request not found!");
            }
            _context.Request.Remove(dbRequest.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.Request.ToListAsync());
        }
    }
}
