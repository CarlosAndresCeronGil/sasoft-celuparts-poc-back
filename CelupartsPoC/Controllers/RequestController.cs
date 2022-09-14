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

        [HttpGet("Retomas/{page}/RequestDate")]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetRetomasByDate(int page, [FromQuery] DateTime InitialDate, [FromQuery] DateTime FinalDate)
        {
            var pageResults = 10f;

            if (FinalDate == DateTime.MinValue) {

                var pageCountWithoutFinalDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Retoma").Where((x => x.RequestDate >= InitialDate)).Count() / pageResults);

                var lastDate = new DateTime(2020, 1, 1);

                var requestsWithoutFinalDate = await _context.Request.AsNoTracking().Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate >= InitialDate))
                .Include(x => x.UserDto)
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                //.Where(x => x.RequestDate > lastDate)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

                var responseWithoutFinalDate = new RequestResponse
                {
                    Requests = requestsWithoutFinalDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutFinalDate
                };

                return Ok(responseWithoutFinalDate);

            } 
            else if (InitialDate == DateTime.MinValue)
            {

                var pageCountWithoutInitialDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Retoma").Where((x => x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

                var lastDate = new DateTime(2020, 1, 1);

                var requestsWithoutInitialDate = await _context.Request.Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate <= FinalDate.AddDays(1)))
                .Include(x => x.UserDto)
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

                var responseWithoutInitialDate = new RequestResponse
                {
                    Requests = requestsWithoutInitialDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutInitialDate
                };

                return Ok(responseWithoutInitialDate);
            }

            var pageCount = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Retoma").Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

            var requests = await _context.Request.Where(req => req.RequestType == "Retoma")
                .Where((x => x.RequestDate >= InitialDate && x.RequestDate  <= FinalDate.AddDays(1)))
                .Include(x => x.UserDto)
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
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

        [HttpGet("Repairs/{page}/RequestDate")]
        public async Task<ActionResult<List<RequestWithEquipments>>> GetRepairsByDate(int page, [FromQuery] DateTime InitialDate, [FromQuery] DateTime FinalDate)
        {
            var pageResults = 10f;

            if (FinalDate == DateTime.MinValue)
            {

                var pageCountWithoutFinalDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate >= InitialDate)).Count() / pageResults);

                var requestsWithoutFinalDate = await _context.Request.Where(req => req.RequestType == "Reparacion")
                .Where((x => x.RequestDate >= InitialDate))
                .Include(x => x.UserDto)
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

                var responseWithoutFinalDate = new RequestResponse
                {
                    Requests = requestsWithoutFinalDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutFinalDate
                };

                return Ok(responseWithoutFinalDate);

            }
            else if (InitialDate == DateTime.MinValue)
            {

                var pageCountWithoutInitialDate = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

                var requestsWithoutInitialDate = await _context.Request.Where(req => req.RequestType == "Reparacion")
                .Where((x => x.RequestDate <= FinalDate.AddDays(1)))
                .Include(x => x.UserDto)
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

                var responseWithoutInitialDate = new RequestResponse
                {
                    Requests = requestsWithoutInitialDate,
                    CurrentPage = page,
                    Pages = (int)pageCountWithoutInitialDate
                };

                return Ok(responseWithoutInitialDate);
            }

            var pageCount = Math.Ceiling(_context.Request.Where(req => req.RequestType == "Reparacion").Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate.AddDays(1))).Count() / pageResults);

            var requests = await _context.Request.Where(req => req.RequestType == "Reparacion")
                .Where((x => x.RequestDate >= InitialDate && x.RequestDate <= FinalDate.AddDays(1)))
                .Include(x => x.UserDto)
                .Include(x => x.Repairs)
                    .ThenInclude(y => y.RepairPayments)
                .Include(x => x.RequestStatus)
                .Include(x => x.HomeServices)
                .Include(x => x.Equipment)
                .Include(x => x.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.RequestNotifications)
                .OrderByDescending(x => x.RequestDate)
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

        [HttpGet("UserInfo/{id}")]
        public async Task<ActionResult<RequestWithEquipments>> GetWithUserInfo(int id)
        {
            //var request = _context.Request.FindAsync(id);
            var request = _context.Request.Where(n => n.IdRequest == id)
                .Include(x => x.UserDto);
            if (request == null)
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
