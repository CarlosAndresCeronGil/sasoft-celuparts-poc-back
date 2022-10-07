using CelupartsPoC.Common;
using Google.Apis.Gmail.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CelupartsPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //public static User user = new User();
        private string urlDomain = "http://localhost:3000/";
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
            try
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
                user.IdUser = userDto.Result.IdUser;
                user.Role = "user";
                user.FullName = request.Names + " " + request.Surnames;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _context.User.Add(user);

                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser(UserDto request)
        {
            //var dbUser = _context.User.FindAsync(request.Email);
            var dbUserDto = _context.UsersDto.Where(x => x.Email == request.Email).FirstOrDefault();
            var dbUser = _context.User.Where(x => x.Email == request.Email).FirstOrDefault();
            if (dbUser.Email != request.Email)
            {
                return BadRequest("User not found!");
            }
            if (dbUserDto.AccountStatus == "Inhabilitada")
            {
                return BadRequest("Account disabled");
            } else if (!VerifyPasswordHash(request.Password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                if (dbUserDto!.LoginAttempts >= 3)
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

        [HttpPost("startRecovery")]
        public async Task<ActionResult<string>> StartRecovery([FromBody] string email)
        {
            try
            {
                string token = GetSha256(Guid.NewGuid().ToString());

                var dbUserDto = _context.UsersDto.Where(x => x.Email == email).FirstOrDefault();

                if (dbUserDto != null)
                {
                    dbUserDto.TokenRecovery = token;
                    await _context.SaveChangesAsync();

                    //Enviar email
                    SendEmail(dbUserDto.Email, token);

                    return Ok("Se editó el Token Recovery");
                }

                return BadRequest("Email not found");
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("recoverPassword")]
        public async Task<ActionResult<string>> Recovery([FromForm] RecoverPassword recoverPassword)
        {
            try
            {
                CreatePasswordHash(recoverPassword.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                var dbUserDto = _context.UsersDto.Where(x => x.TokenRecovery == recoverPassword.Token).FirstOrDefault();

                if(dbUserDto != null)
                {
                    dbUserDto.Password = CommonMethods.ConvertToEncrypt(recoverPassword.NewPassword);
                    await _context.SaveChangesAsync();

                    var user = _context.User.Where(x => x.Email == dbUserDto.Email).FirstOrDefault();

                    user!.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    await _context.SaveChangesAsync();

                    return Ok("Password succesfully updated");
                }

                return BadRequest("User not found!");

            } catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /*[HttpPost("register/Google")]
        public async Task<ActionResult<string>> LoginWithGoogle([FromForm] UserDto googleData)
        {
            /* RECIBE LOS DATOS DE LA API DE GOOGLE
            return Ok();
        }*/

        /*[HttpPost("register/Google")]
        public async Task<ActionResult> GoogleAthenticate([FromBody] GoogleUser request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));
            }
            return Ok(GenerateUserToken(await _userService.AuthenticateGoogleUserAsync(request)));
        }*/

        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnurl = null)
        {
            return Challenge(provider, returnurl);
        }*/


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("name", user.FullName),
                new Claim("role", user.Role),
                new Claim("idUser", user.IdUser.ToString()),
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

        /*private UserToken GenerateUserToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Jwt:Secret"]);

            var expires = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id) ,
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Authentication:Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Surname, user.FirstName),
                    new Claim(ClaimTypes.GivenName, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                }),

                Expires = expires,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Authentication:Jwt:Issuer"],
                Audience = _configuration["Authentication:Jwt:Audience"]
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return new UserToken
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token,
                Expires = expires
            };
        }*/

        #region HELPERS

        private string GetSha256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void SendEmail(string emailDestino, string token)
        {

            string emailOrigen = "andrescerontest@gmail.com";
            //string contraseña = "A12345678!";
            string contraseña = "trfsieetqmapytxq";

            string url = urlDomain + "changepassword/" + token;

            MailMessage oMailMessage = new MailMessage(
                emailOrigen, emailDestino, 
                "Recuperación de contraseña", 
                "<h1>Esto es un mensaje de prueba para recuperacion de contraseña</h1><br>"+
                "<a href='"+url+"'>Click para recuperar</a>");

            oMailMessage.IsBodyHtml = true;


            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, contraseña);

            oSmtpClient.Send(oMailMessage);

            oSmtpClient.Dispose();
        }

        #endregion
    }
}
