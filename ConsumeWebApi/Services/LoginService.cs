using System;
using System.Collections.Generic;
using StockWebApi.Models;
using StockWebApi.Models.Login;
using StockWebApi.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Services
{
    public class LoginService: ILoginService
    {
        private readonly IRepository<User> _repo;
        private readonly IPasswordHasher _hasher;
        public LoginService(IRepository<User> repository,IPasswordHasher hasher){
            _repo = repository;
            _hasher = hasher;
        }
        public async Task<LoginResult> Login(LoginRequest request){
            var user=_repo.GetByUserName(request.UserName);
            if(user==null){
                return null;
            }
            else{
                var result=_hasher.VerifyPassword(user.Password,request.Password);
                return new LoginResult{
                    isLogin=result,
                };
            }

        }

        
    }
}