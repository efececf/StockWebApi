using System;
using System.Collections.Generic;
using StockWebApi.Models;
using StockWebApi.Models.Login;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StockWebApi.Interfaces;

namespace StockWebApi.Services
{
    public class LoginService: ILoginService
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _hasher;
        private ITokenService _tokenService;
        public LoginService(IUserRepository repository,IPasswordHasher hasher,ITokenService tokenService){
            _repo = repository;
            _hasher = hasher;
            _tokenService = tokenService;
        }
        public async Task<AuthToken> Login(LoginRequest request){
            var user=await _repo.GetByUserName(request.UserName);
            if(user==null){
                return null;
            }
            else{
                var result=_hasher.VerifyPassword(user.PasswordHash,request.Password);
                if(result==true){
                    new LoginResult{
                    IsLogin=result,
                    }; 
                    var token= _tokenService.GenerateToken(user);
                    return token;
                }
                else{
                    return null;
                }
            }

        }
        
    }
}