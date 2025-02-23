using System;
using StockWebApi.Context;
using StockWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Services
{
    public class RegisterService:IRegisterService
    {
        private readonly UserRepository _repo;
        public RegisterService(UserRepository repo){
            _repo=repo;
        }
        public async Task<RegisterResult> Register(RegisterRequest request){
            var user=_repo.GetByUserName(request.UserName);
            if(user!=null){
                return async new RegisterResult{
                    IsRegistered=false,
                };
            }
            else{
                var newUser = new User{
                    UserName = request.UserName,
                    PasswordHash=request.PasswordHash,
                    Name=request.Name
                };
                async _repo.Add(newUser);
                return async new RegisterResult{
                    IsRegistered=true,
                };
            }
            
        }
        
    }
}