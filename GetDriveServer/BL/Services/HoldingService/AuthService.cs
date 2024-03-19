using AutoMapper;
using BL.DTOs;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.HoldingService
{
    public interface IAuthService
    {
        ResponseDto<AuthDTO> Login(LoginDto loginDto);
    }

    public class AuthService : IAuthService
    {
        private readonly IRepository<User> userRepo;

        public AuthService(IRepository<User> userRepo) 
        {
            this.userRepo = userRepo;
        }

        public ResponseDto<AuthDTO> Login(LoginDto loginDto)
        {
            var user = userRepo.GetQueryable().Where(u => u.Name == loginDto.Username && u.Password == loginDto.Password).FirstOrDefault();
            if (user == null)
            {
                return new ResponseDto<AuthDTO> { status = 401, message = "Username and password combination not correct.", result = new AuthDTO { } };
            }
            return new ResponseDto<AuthDTO> { status = 200, message = "Login successfull", result = new AuthDTO { UserId = user.Id, UserName = user.Name } };
        }
    }
}
