using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestNotificationController : ControllerBase
    {
        private readonly DataContext _context;

        public RequestNotificationController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<RequestNotification>>> Get()
        {
            var requestNotifications = _context.RequestNotification.ToList();
            return Ok(requestNotifications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestNotification>> Get(int id)
        {
            var requestNotifications = _context.RequestNotification.FindAsync(id);
            if (requestNotifications.Result == null)
            {
                return BadRequest("Request notification not found!");
            }
            return Ok(requestNotifications.Result);
        }

        [HttpPost]
        public async Task<ActionResult<List<RequestNotification>>> AddRequestNotification(RequestNotification requestNotification)
        {
            _context.RequestNotification.Add(requestNotification);
            await _context.SaveChangesAsync();

            return Ok(await _context.Repair.FindAsync(requestNotification.IdRequestNotification));
        }

        [HttpPut]
        public async Task<ActionResult<List<RequestNotification>>> UpdateRequestNotification(RequestNotification requestNotification)
        {
            var dbRequestNotification = _context.RequestNotification.FindAsync(requestNotification.IdRequestNotification);
            if(dbRequestNotification.Result == null)
            {
                return BadRequest("Request notification not found!");
            }
            dbRequestNotification.Result.IdRequest = requestNotification.IdRequest;
            dbRequestNotification.Result.Message = requestNotification.Message;
            dbRequestNotification.Result.HideNotification = requestNotification.HideNotification;
            dbRequestNotification.Result.NotificationType = requestNotification.NotificationType;

            await _context.SaveChangesAsync();

            return Ok(await _context.RequestNotification.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RequestNotification>>> Delete(int id)
        {
            var dbRequestNotification = _context.RequestNotification.FindAsync(id);
            if(dbRequestNotification.Result == null)
            {
                return BadRequest("Request notification not found!");
            }
            _context.RequestNotification.Remove(dbRequestNotification.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.RequestNotification.ToListAsync());
        }
    }
}
