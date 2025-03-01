using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using YouTubeWebAPI.Models.Auhentication;
using System.Security.Claims;

namespace YouTubeWebAPI.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _configuration;

        public JWTManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Tokens Authenticate(User users)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ed474c2bef330250c15796e64780c56a690f585f");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.UserName),
                    new Claim(ClaimTypes.Email, users.Email),
                    new Claim(ClaimTypes.NameIdentifier, users.UserId),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}