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
        private readonly IUserRepository _repo;
        private readonly IPortfolioRepository _portRepo;
        private IPasswordHasher _passwordHasher;
        public RegisterService(IUserRepository repo,IPortfolioRepository portRepo,IPasswordHasher passwordHasher){
            _repo=repo;
            _portRepo=portRepo;
            _passwordHasher=passwordHasher;
        }
        public async Task<RegisterResult> Register(RegisterRequest request){
            var user=await _repo.GetByUserName(request.Username);
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
                    UserName = request.Username,
                    PasswordHash=_passwordHasher.Hash(request.Password),
                    Name=request.Name,
                    UserRole=request.UserRole,
                };
                await _repo.Add(newUser);
                var newPort=new Portfolio{
                    Name="Portfolio",
                    Profit=0,
                    UserId=newUser.Id,
                    Stocks = new List<StockPortfolio>()
                };
                await _portRepo.Add(newPort);
                var result= new RegisterResult
                {
                    IsRegistered=true,
                };
                return result;
            }
            
        }
        
    }
}