using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDtoController : ControllerBase
    {
        //public static List<UserDto> users = DataList.users.ToList();
        private readonly DataContext _context;

        public UserDtoController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            /*var userDtoWithRequests = _context.UsersDto.Select(userDto => new UserDtoWithRequests()
            {
                IdUser = userDto.IdUser,
                IdType = userDto.IdType,
                IdNumber = userDto.IdNumber,
                Names = userDto.Names,
                Surnames = userDto.Surnames,
                Phone = userDto.Phone,
                AlternativePhone = userDto.AlternativePhone,
                Email = userDto.Email,
                AccountStatus = userDto.AccountStatus,
                Requests = userDto.Requests.Select(n => n).ToList()
            }).ToList();*/
            var userDtoWithRequests = _context.UsersDto
                .Include(x => x.Requests)
                    .ThenInclude(y => y.RequestStatus)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.Repairs)
                    .ThenInclude(w => w.RepairPayments)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.HomeServices)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.Equipment)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.RequestNotifications);

            return Ok(userDtoWithRequests);
        }

        [HttpGet("SimpleInfo")]
        public async Task<ActionResult<List<UserDto>>> GetSimpleInfo()
        {
            var usersDtoWithRoles = _context.UsersDto.FromSqlRaw($"select UD.IdUser, UD.IdNumber, UD.IdType, UD.LoginAttempts, UD.Names, UD.Surnames, UD.Email, UD.Phone, UD.AlternativePhone, UD.AccountStatus, UD.Password, UD.TokenRecovery, U.Role from UsersDto as UD join [User] as U on U.IdUser = UD.IdUser where U.Role = 'user'");

            return Ok(usersDtoWithRoles);
        }

        [HttpGet("LastRequestInfo/Repair/{id}")]
        public async Task<ActionResult<List<UserDto>>> GetLastAddressRepair(int id)
        {
            try
            {
                var usersLastAddress = _context.UsersDto.Where(x => x.IdUser == id)
                    .Include(x => x.Requests.Where(x => x.RequestType == "Reparacion").OrderByDescending(x => x.IdRequest).Take(1));
                return Ok(usersLastAddress);
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("LastRequestInfo/Retoma/{id}")]
        public async Task<ActionResult<List<UserDto>>> GetLastAddressRetoma(int id)
        {
            try
            {
                var usersLastAddress = _context.UsersDto.Where(x => x.IdUser == id)
                    .Include(x => x.Requests.Where(x => x.RequestType == "Retoma").OrderByDescending(x => x.IdRequest).Take(1));
                return Ok(usersLastAddress);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            /*var userDtoWithRequests = _context.UsersDto.Where(n => n.IdUser == id).Select(userDto => new UserDtoWithRequests()
            {
                IdUser = userDto.IdUser,
                IdType = userDto.IdType,
                Names = userDto.Names,
                Surnames = userDto.Surnames,
                Phone = userDto.Phone,
                AlternativePhone = userDto.AlternativePhone,
                Email = userDto.Email,
                AccountStatus = userDto.AccountStatus,
                Requests = userDto.Requests.Select(n => n).ToList()
            }).FirstOrDefault();*/
            var userDto = _context.UsersDto.Where(n => n.IdUser == id)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.RequestStatus)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.Repairs)
                    .ThenInclude(w => w.RepairPayments)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.HomeServices)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.Equipment)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.Retoma)
                    .ThenInclude(y => y.RetomaPayments)
                .Include(x => x.Requests)
                    .ThenInclude(y => y.RequestNotifications);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<List<UserDto>>> AddUser(UserDto user)
        {
            _context.UsersDto.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.UsersDto.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<UserDto>>> UpdateUser(UserDto request)
        {
            var dbUser = _context.UsersDto.FindAsync(request.IdUser);
            if (dbUser.Result == null)
            {
                return BadRequest("User not found!");
            }
            dbUser.Result.IdType = request.IdType;
            dbUser.Result.Names = request.Names;
            dbUser.Result.Surnames = request.Surnames;
            dbUser.Result.Phone = request.Phone;
            dbUser.Result.AlternativePhone = request.AlternativePhone;
            dbUser.Result.Email = request.Email;
            dbUser.Result.AccountStatus = request.AccountStatus;
            dbUser.Result.IdNumber = request.IdNumber;
            dbUser.Result.LoginAttempts = request.LoginAttempts;

            await _context.SaveChangesAsync();

            return Ok(await _context.UsersDto.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserDto>>> Delete(int id)
        {
            var dbUser = _context.UsersDto.FindAsync(id);
            if (dbUser.Result == null)
            {
                return BadRequest("User not found!");
            }
            _context.UsersDto.Remove(dbUser.Result);
            await _context.SaveChangesAsync();
            return Ok(await _context.UsersDto.ToListAsync());
        }
    }
}
