using CelupartsPoC.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AuthController(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            
            //Creating two users, one for login other for compilation of data
            User user = new User();
            UserDto NewUserDto = new UserDto();

            NewUserDto.IdType = request.IdType;
            NewUserDto.IdNumber = request.IdNumber;
            NewUserDto.Names = request.Names;
            NewUserDto.Surnames = request.Surnames;
            NewUserDto.Phone = request.Phone;
            NewUserDto.AlternativePhone = request.AlternativePhone;
            NewUserDto.Email = request.Email;
            NewUserDto.Password = CommonMethods.ConvertToEncrypt(request.Password);
            NewUserDto.AccountStatus = request.AccountStatus;
            NewUserDto.LoginAttempts = 0;


            _context.UsersDto.Add(NewUserDto);
            //_context.UsersDto.Add(request);

            await _context.SaveChangesAsync();

            var userDto = _context.UsersDto.FindAsync(NewUserDto.IdUser);

            user.Email = request.Email;
            user.IdUserDto = userDto.Result.IdUser;
            user.Role = "user";
            user.FullName = request.Names + " " + request.Surnames;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.User.Add(user);
            
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser(UserDto request)
        {
            //var dbUser = _context.User.FindAsync(request.Email);
            var dbUserDto = _context.UsersDto.Where(x => x.Email == request.Email).FirstOrDefault();
            var dbUser = _context.User.Where(x => x.Email == request.Email).FirstOrDefault();
            if(dbUser.Email != request.Email)
            {
                return BadRequest("User not found!");
            }
            if (dbUserDto.AccountStatus == "Inhabilitada")
            {
                return BadRequest("Account disabled");
            } else if (!VerifyPasswordHash(request.Password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                if(dbUserDto!.LoginAttempts >= 3)
                {
                    dbUserDto.AccountStatus = "Inhabilitada";
                    await _context.SaveChangesAsync();
                    return BadRequest("Account disabled");
                } else
                {
                    dbUserDto!.LoginAttempts = dbUserDto.LoginAttempts + 1;
                }
                await _context.SaveChangesAsync();
                return BadRequest("Wrong password!");
            } else
            {
                dbUserDto!.LoginAttempts = 0;
                await _context.SaveChangesAsync();
            }

            string token = CreateToken(dbUser);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("name", user.FullName),
                new Claim("role", user.Role),
                new Claim("idUser", user.IdUserDto.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
