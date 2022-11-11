using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestStatusController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestStatus>>> Get()
        {
            return Ok(await _context.RequestStatus.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestStatus>> Get(int id)
        {
            var requestState = _context.RequestStatus
                .Include(x => x.Request)
                .FirstOrDefaultAsync(i => i.IdRequestStatus == id );
            if (requestState.Result == null)
            {
                return BadRequest("Request state not found!");
            }
            return Ok(requestState.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<RequestStatus>>> AddRequestState(RequestStatus requestState)
        {
            _context.RequestStatus.Add(requestState);
            await _context.SaveChangesAsync();

            return Ok(await _context.RequestStatus.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<RequestStatus>>> UpdateRequestState(RequestStatus request)
        {
            var dbRequestState = _context.RequestStatus.FindAsync(request.IdRequestStatus);
            if (dbRequestState.Result == null)
            {
                return BadRequest("Request state not found!");
            }
            dbRequestState.Result.IdRequest = request.IdRequest;
            dbRequestState.Result.Status = request.Status;
            dbRequestState.Result.PaymentStatus = request.PaymentStatus;
            dbRequestState.Result.ProductReturned = request.ProductReturned;
            dbRequestState.Result.ProductSold = request.ProductSold;

            await _context.SaveChangesAsync();

            return Ok(await _context.RequestStatus.FindAsync(request.IdRequestStatus));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RequestStatus>>> Delete(int id)
        {
            var dbRequestState = _context.RequestStatus.FindAsync(id);
            if (dbRequestState.Result == null)
            {
                return BadRequest("Request state not found!");
            }
            _context.RequestStatus.Remove(dbRequestState.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.RequestStatus.ToListAsync());
        }
    }
}
