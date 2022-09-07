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

        [HttpGet("Retomas/{page}")]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetRetomas(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Retoma").Count() / pageResults);

            var requests = await _context.Request.Where(req => req.RequestType == "Retoma")
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderBy(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new RequestResponse
            {
                Requests = requests,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpGet("Retomas/RequestDate")]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetRetomasByDate([FromQuery] DateTime InitialDate, [FromQuery] DateTime FinalDate)
        {
            if(FinalDate == DateTime.MinValue) {
                var requestsWithoutFinalDate = await _context.Request.Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate >= InitialDate))
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderBy(x => x.RequestDate)
                .ToListAsync();

                return Ok(requestsWithoutFinalDate);

            } 
            else if (InitialDate == DateTime.MinValue)
            {
                var requestsWithoutInitialDate = await _context.Request.Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate <= FinalDate.AddDays(1)))
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderBy(x => x.RequestDate)
                .ToListAsync();

                return Ok(requestsWithoutInitialDate);
            }
            var requests = await _context.Request.Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate >= InitialDate && x.RequestDate  <= FinalDate.AddDays(1)))
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderBy(x => x.RequestDate)
                .ToListAsync();

            return Ok(requests);
        }

        [HttpGet("Repairs/{page}")]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetRepairs(int page)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Count() / pageResults);

            var requests = await _context.Request.Where(req => req.RequestType == "Reparacion")
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderBy(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new RequestResponse
            {
                Requests = requests,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetAll()
        {
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
            //var request = _context.Request.FindAsync(id);
            var request = _context.Request.Where(n => n.IdRequest == id)
                .Include(x => x.HomeServices)
                .Include(x => x.RequestStatus);
            if(request == null)
            {
                return BadRequest("Request not found!");
            }
            return Ok(request);
        }

        [HttpPost]
        public async Task<ActionResult<List<RequestWithEquipments>>> AddUser(RequestWithEquipments request)
        {
            //request.RequestDate = DateTime.UtcNow.Date; Devuelve 2022-09-05 00:00:00.0000000
            //request.RequestDate = DateTime.Today; Devuelve 2022-09-05 00:00:00.0000000
            request.RequestDate = DateTime.Today;
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
