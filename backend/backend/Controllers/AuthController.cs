using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly AppContext context;

        public AuthController(AppContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<string> Auth([FromBody] AuthData data)
        {
            var user = Authenticate(data);
            if (user == null)
                return Unauthorized();
            return GenerateToken(user);
        }

        private User Authenticate(AuthData data)
        {
            if (string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Password))
                return null;
            var sha256 = SHA256.Create();
            var hashed = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(data.Password)));
            var user = (from c in context.Credentials
                        join u in context.Users on c.UserId equals u.Id
                        where u.Name == data.Name && c.PasswordHash == hashed
                        select u).FirstOrDefault();
            return user;
        }

        private string GenerateToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            var signingKey = Environment.GetEnvironmentVariable("SECRET_KEY");
            var token = new JwtSecurityToken(
                issuer: "cinnabuns",
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class AuthData
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}