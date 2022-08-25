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
        public async Task<ActionResult<List<RequestWithEquipments>>> Get()
        {
            /*var requestWithEquipments = _context.Request.Select(request => new RequestWithoutCycle()
            {
                IdRequest = request.IdRequest,
                IdUser = request.IdUser,
                IdEquipment = request.IdEquipment,
                RequestType = request.RequestType,
                PickUpAddress = request.PickUpAddress,
                DeliveryAddress = request.DeliveryAddress,
                StatusQuote = request.StatusQuote,
                RequestStatus = request.RequestStatus.Select(n => n).ToList(),
            }).ToList();*/
            var requests = _context.Request
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications);
            return Ok(requests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestWithEquipments>> Get(int id)
        {
            var request = _context.Request.FindAsync(id);
            if(request.Result == null)
            {
                return BadRequest("Request not found!");
            }
            return Ok(request.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<RequestWithEquipments>>> AddUser(RequestWithEquipments request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();

            return Ok(await _context.Request.FindAsync(request.IdRequest));
        }

        [HttpPut]
        public async Task<ActionResult<List<RequestWithEquipments>>> UpdateRequest(RequestWithEquipments requestR)
        {
            var dbRequest = _context.Request.FindAsync(requestR.IdRequest);
            if (dbRequest.Result == null)
            {
                return BadRequest("Request not found!");
            }
            dbRequest.Result.IdUser = requestR.IdUser;
            dbRequest.Result.IdEquipment = requestR.IdEquipment;
            dbRequest.Result.RequestType = requestR.RequestType;
            dbRequest.Result.PickUpAddress = requestR.PickUpAddress;
            dbRequest.Result.DeliveryAddress = requestR.DeliveryAddress;
            dbRequest.Result.StatusQuote = requestR.StatusQuote;

            await _context.SaveChangesAsync();

            return Ok(await _context.Request.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RequestWithEquipments>>> Delete(int id)
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
