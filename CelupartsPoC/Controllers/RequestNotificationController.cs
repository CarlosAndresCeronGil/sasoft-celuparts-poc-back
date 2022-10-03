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

        [HttpGet("Admin")]
        public async Task<ActionResult<List<RequestNotification>>> GetAdminNotifications()
        {
            var requestNotifications = _context.RequestNotification.Where(x => x.NotificationType == "to_admin").OrderByDescending(x => x.IdRequestNotification).ToList();
            return Ok(requestNotifications);
        }

        [HttpGet("Admin/FirstThree")]
        public async Task<ActionResult<List<RequestNotification>>> GetThreeAdminNotifications()
        {
            var requestNotifications = _context.RequestNotification.Where(x => x.NotificationType == "to_admin").OrderByDescending(x => x.IdRequestNotification).Take(3).ToList();
            return Ok(requestNotifications);
        }

        [HttpGet("Technician")]
        public async Task<ActionResult<List<RequestNotification>>> GetTechnicianNotifications()
        {
            var requestNotifications = _context.RequestNotification.Where(x => x.NotificationType == "to_technician").OrderByDescending(x => x.IdRequestNotification).ToList();
            return Ok(requestNotifications);
        }

        [HttpGet("Technician/FirstThree")]
        public async Task<ActionResult<List<RequestNotification>>> GetThreeTechnicianNotifications()
        {
            var requestNotifications = _context.RequestNotification.Where(x => x.NotificationType == "to_technician").OrderByDescending(x => x.IdRequestNotification).Take(3).ToList();
            return Ok(requestNotifications);
        }

        [HttpGet("Courier")]
        public async Task<ActionResult<List<RequestNotification>>> GetCourierNotifications()
        {
            var requestNotifications = _context.RequestNotification.Where(x => x.NotificationType == "to_courier").OrderByDescending(x => x.IdRequestNotification).ToList();
            return Ok(requestNotifications);
        }

        [HttpGet("Courier/FirstThree")]
        public async Task<ActionResult<List<RequestNotification>>> GetThreeCourierNotifications()
        {
            var requestNotifications = _context.RequestNotification.Where(x => x.NotificationType == "to_courier").OrderByDescending(x => x.IdRequestNotification).Take(3).ToList();
            return Ok(requestNotifications);
        }

        [HttpGet("Request/{idUserDto}")]
        public async Task<ActionResult<List<RequestNotification>>> GetCustomerNotifications(int idUserDto)
        {
            //var requestNotifications = _context.RequestNotification.Where(x => x.NotificationType == "to_courier").ToList();
            //var requestNotifications = _context.RequestNotification.FromSqlRaw($"select RN.IdRequestNotification, RN.IdRequest, RN.Message, RN.WasReviewed, RN.NotificationType from RequestNotification as RN join Request as R on R.IdRequest=RN.IdRequest where R.IdRequest={idRequest}");
            var requestNotifications = _context.RequestNotification.FromSqlRaw($"select RN.IdRequestNotification, RN.IdRequest, RN.Message, RN.WasReviewed, RN.NotificationType from RequestNotification as RN join Request as R on R.IdRequest = RN.IdRequest join UsersDto as UD on UD.IdUser = R.IdUser where UD.IdUser ={idUserDto} and RN.NotificationType = 'to_customer'");
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
            dbRequestNotification.Result.WasReviewed = requestNotification.WasReviewed;
            dbRequestNotification.Result.NotificationType = requestNotification.NotificationType;

            await _context.SaveChangesAsync();

            //return Ok(await _context.RequestNotification.ToListAsync());
            return Ok(await _context.RequestNotification.FindAsync(requestNotification.IdRequestNotification));
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
