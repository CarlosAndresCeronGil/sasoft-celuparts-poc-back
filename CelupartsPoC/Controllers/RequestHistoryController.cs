using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestHistoryController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestHistoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestHistory>>> Get()
        {
            var requestHistory = _context.RequestHistory.ToList();
            return Ok(requestHistory);
        }

        [HttpGet("idRequest/{idRequest}")]
        public async Task<ActionResult<List<RequestHistory>>> Get(int idRequest)
        {
            var requestHistory = _context.RequestHistory.Where(x => x.IdRequest == idRequest).ToList();
            return Ok(requestHistory);
        }

        [HttpPost]
        public async Task<ActionResult<List<RequestHistory>>> AddRequestHistory(RequestHistory requestHistory)
        {
            var searchRequestHistory = _context.RequestHistory.Where(x => x.IdRequest == requestHistory.IdRequest && x.Status == requestHistory.Status);
            if(!searchRequestHistory.Any())
            {
                _context.RequestHistory.Add(requestHistory);

                await _context.SaveChangesAsync();

                return Ok(await _context.RequestHistory.FindAsync(requestHistory.IdRequestHistory));
            }else
            {
                return Ok("Dato repetido");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<RequestHistory>>> UpdateRequestHistory(RequestHistory requestHistory)
        {
            var dbRequestHistory = _context.RequestHistory.FindAsync(requestHistory.IdRequestHistory);
            if(dbRequestHistory.Result == null)
            {
                return BadRequest("Request history not found!");
            }
            dbRequestHistory.Result.IdRequest = requestHistory.IdRequest;
            dbRequestHistory.Result.Status = requestHistory.Status;
            dbRequestHistory.Result.Date = requestHistory.Date;

            await _context.SaveChangesAsync();

            return Ok(await _context.RequestHistory.FindAsync(requestHistory.IdRequestHistory));
        }

        [HttpDelete]
        public async Task<ActionResult<List<RequestHistory>>> Delete(int id)
        {
            var dbRequestNotification = _context.RequestHistory.FindAsync(id);
            if(dbRequestNotification.Result == null)
            {
                return BadRequest("Request history not found!");
            }
            _context.RequestHistory.Remove(dbRequestNotification.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.RequestHistory.ToListAsync());
        }
    }
}
