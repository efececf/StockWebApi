using System;
using StockWebApi.Context;
using StockWebApi.Models;
using StockWebApi.Models.Register;
using StockWebApi.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockWebApi.Services
{
    public class RegisterService: IRegisterService
    {
        private readonly IRepository<User> _repo;
        private IPasswordHasher _passwordHasher;
        public RegisterService(IRepository<User> repo,IPasswordHasher passwordHasher){
            _repo=repo;
            _passwordHasher=passwordHasher;
        }
        public async Task<RegisterResult> Register(RegisterRequest request){
            var user=_repo.GetByUserName(request.UserName);
            if(user!=null){
                var result= new RegisterResult
                {
                    IsRegistered=false,
                };
                return result;
            }
            else{
                var newUser = new User
                {
                    UserName = request.UserName,
                    PasswordHash=_passwordHasher.Hash(request.Password),
                    Name=request.Name,
                };
                _repo.Add(newUser);
                var result= new RegisterResult
                {
                    IsRegistered=true,
                };
                return result;
            }
            
        }
        
    }
}