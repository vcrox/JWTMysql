using JWTMysql.Models;
using JWTMysql.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTMysql.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<ServiceResponse<ResponseLogin>> LoginAsync(string email, string password)
        {
            var response = new ServiceResponse<ResponseLogin>();

            var existingUser = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (existingUser != null)
            {
                var isPasswordVerified = CryptoUtil.VerifyPassword(password, existingUser.Salt, existingUser.Password);
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
                    response.Success = true;
                    var data = new ResponseLogin()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ExpireDate = timeStamp
                    };
                    response.Data = data;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Password is wrong";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Email is wrong";
            }
            return response;
        }

        public async Task<ServiceResponse<string>> Register(string email, string password)
        {
            var response = new ServiceResponse<string>();
            if (!_context.Users.Any(x => x.Email == email))
            {
                //var email = email;
                var salt = CryptoUtil.GenerateSalt();
                //var password = requestRegister.Password;
                var hashedPassword = CryptoUtil.HashMultiple(password, salt);
                var user = new User();
                user.Email = email;
                user.Salt = salt;
                user.Password = hashedPassword;
                user.Role = "USER";
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Data = email;
            }
            else
            {
                response.Success = false;
                response.Message = "Email is already in use";
            }
            return response;
        }
    }
}
