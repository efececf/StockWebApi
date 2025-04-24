using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using StockWebApi.Models;
using StockWebApi.Interfaces;
using System.IdentityModel;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;



namespace StockWebApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private string SecurityKey="thisisaverylongsecretkeywith32characters!";
        public TokenService(IConfiguration config){
            _config = config;
        }
        public AuthToken GenerateToken(User user){
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),///Burda claim json şeklinde tutuluyor yani string olması lazım ki json a dönsün o yüzxdenn stringe döndürmeli
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName),
                new Claim(ClaimTypes.Role,user.UserRole),//Role için ClaimType şeklinde
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };
            var token = new JwtSecurityToken(
                issuer:"http://localhost:5001",
                audience:"http://localhost:5001",
                claims:claims,
                expires:DateTime.UtcNow.AddHours(3),
                signingCredentials:creds
            );
            var TokenString= new JwtSecurityTokenHandler().WriteToken(token);//oluşan tokeni stringe döndürür
            var authtoken=new AuthToken{
                Token=TokenString,
                UserId=user.Id,
                UserName=user.UserName,
                Expiration=DateTime.UtcNow.AddHours(3),
                UserRole=user.UserRole
            };
            return authtoken;

        }
        
    }
}