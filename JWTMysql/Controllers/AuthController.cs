using JWTMysql.Data;
using JWTMysql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace JWTMysql.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepo;

        public AuthController(IConfiguration configuration, IAuthRepository authRepo)
        {
            _configuration = configuration;
            _authRepo = authRepo;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(RequestLogin requestLogin)
        {
            /*
            var responseLogin = new ResponseLogin();
            using (var db = new NetCoreAuthJwtMySqlContext())
            {
                var existingUser = db.Users.SingleOrDefault(x => x.Email == requestLogin.Email);
                if (existingUser != null)
                {
                    var isPasswordVerified = CryptoUtil.VerifyPassword(requestLogin.Password, existingUser.Salt, existingUser.Password);
                    if (isPasswordVerified)
                    {
                        var claimList = new List<Claim>();
                        claimList.Add(new Claim(ClaimTypes.Name, existingUser.Email));
                        claimList.Add(new Claim(ClaimTypes.Role, existingUser.Role));
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var expireDate = DateTime.UtcNow.AddDays(1);
                        var timeStamp = DateUtil.ConvertToTimeStamp(expireDate);
                        var token = new JwtSecurityToken(
                            claims: claimList,
                            notBefore: DateTime.UtcNow,
                            expires: expireDate,
                            signingCredentials: creds);
                        responseLogin.Success = true;
                        responseLogin.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        responseLogin.ExpireDate = timeStamp;
                    }
                    else
                    {
                        responseLogin.MessageList.Add("Password is wrong");
                    }
                }
                else
                {
                    responseLogin.MessageList.Add("Email is wrong");
                }
            }
            return responseLogin;
            */
            var response = await _authRepo.LoginAsync(requestLogin.Email, requestLogin.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RequestRegister requestRegister)
        {
            /*
            var responseRegister = new ResponseRegister();
            using (var db = new NetCoreAuthJwtMySqlContext())
            {
                if (!db.Users.Any(x => x.Email == requestRegister.Email))
                {
                    var email = requestRegister.Email;
                    var salt = CryptoUtil.GenerateSalt();
                    var password = requestRegister.Password;
                    var hashedPassword = CryptoUtil.HashMultiple(password, salt);
                    var user = new User();
                    user.Email = email;
                    user.Salt = salt;
                    user.Password = hashedPassword;
                    user.Role = "USER";
                    db.Users.Add(user);
                    db.SaveChanges();
                    responseRegister.Success = true;
                }
                else
                {
                    responseRegister.MessageList.Add("Email is already in use");
                }
            }
            return responseRegister;
            */
            var response = await _authRepo.Register(requestRegister.Email, requestRegister.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
