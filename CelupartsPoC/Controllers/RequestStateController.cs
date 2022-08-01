using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestStateController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestStateController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestState>>> Get()
        {
            return Ok(await _context.RequestStates.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestState>> Get(int id)
        {
            var requestState = _context.RequestStates.FindAsync(id);
            if (requestState.Result == null)
            {
                return BadRequest("Request state not found!");
            }
            return Ok(requestState.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<RequestState>>> AddRequestState(RequestState requestState)
        {
            _context.RequestStates.Add(requestState);
            await _context.SaveChangesAsync();

            return Ok(await _context.RequestStates.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<RequestState>>> UpdateRequestState(RequestState request)
        {
            var dbRequestState = _context.RequestStates.FindAsync(request.IdRequestState);
            if (dbRequestState.Result == null)
            {
                return BadRequest("Request state not found!");
            }
            dbRequestState.Result.IdRequest = request.IdRequest;
            dbRequestState.Result.PaymentStatus = request.PaymentStatus;
            dbRequestState.Result.ProductReturned = request.ProductReturned;

            await _context.SaveChangesAsync();

            return Ok(await _context.RequestStates.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RequestState>>> Delete(int id)
        {
            var dbRequestState = _context.RequestStates.FindAsync(id);
            if (dbRequestState.Result == null)
            {
                return BadRequest("Request state not found!");
            }
            _context.RequestStates.Remove(dbRequestState.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.RequestStates.ToListAsync());
        }
    }
}
