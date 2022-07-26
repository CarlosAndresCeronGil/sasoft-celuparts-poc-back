using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDtoController : ControllerBase
    {
        public static List<UserDto> users = DataList.users.ToList();

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = users.Find(u => u.IdUser == id);
            if(user == null)
            {
                return BadRequest("User not found!");
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<UserDto>>> AddUser(UserDto user)
        {
            users.Add(user);
            return Ok(users);
        }

        [HttpPut]
        public async Task<ActionResult<List<UserDto>>> UpdateUser(UserDto request)
        {
            var user = users.Find(u => u.IdUser == request.IdUser);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            user.IdType = request.IdType;
            user.Names = request.Names;
            user.Surnames = request.Surnames;
            user.Phone = request.Phone;
            user.AlternativePhone = request.AlternativePhone;
            user.Email = request.Email;

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserDto>>> Delete(int id)
        {
            var user = users.Find(u => u.IdUser == id);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            users.Remove(user);
            return Ok(users);
        }
    }
}
