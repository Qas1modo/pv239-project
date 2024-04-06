using AutoMapper;
using BL.DTOs;
using DAL.Models;
using DAL.Repository;
using DAL.UnitOfWork.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IAuthService
    {
        AuthDTO? Login(LoginDto loginDto);
        Task<bool> ChangePasswordAsync(ChangePasswordDTO input);
        Task<AuthDTO?> RegisterUserAsync(RegistrationDTO registrationDTO);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private const int saltSize = 16;
        private const int iterations = 10000;
        private const int hashLen = 64;
        public AuthService(IUnitOfWork uow, IMapper mapper, 
            IConfiguration configuration)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public AuthDTO? Login(LoginDto loginDto)
        {
            var user = uow.UserRepository.GetQueryable().Where(u => u.Email == loginDto.Email).FirstOrDefault();
            if (user == null || !VerifyPassword(user.Password, user.Salt, loginDto.Password))
            {
                return null;
            }
            return new AuthDTO { UserId = user.Id, UserName = user.Name, Token = GenerateToken(user)};
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
        {
            User user = await uow.UserRepository.GetByID(changePasswordDTO.UserId);
            if (user == null || !VerifyPassword(user.Password, user.Salt, changePasswordDTO.OldPassword))
            {
                return false;
            }
            using (var hashedPassword = new Rfc2898DeriveBytes(changePasswordDTO.NewPassword, saltSize, iterations, HashAlgorithmName.SHA256))
            {
                user.Salt = Convert.ToBase64String(hashedPassword.Salt);
                user.Password = Convert.ToBase64String(hashedPassword.GetBytes(hashLen));
            }
            uow.UserRepository.Update(user);
            await uow.CommitAsync();
            return true;
        }

        public async Task<AuthDTO?> RegisterUserAsync(RegistrationDTO registrationDTO)
        {
            User user = mapper.Map<User>(registrationDTO);
            if (uow.UserRepository.GetQueryable().Any(u => u.Email == registrationDTO.Email || u.Name == registrationDTO.Name))
            {
                return null;
            }
            using (var hashedPassword = new Rfc2898DeriveBytes(registrationDTO.Password, saltSize, iterations, HashAlgorithmName.SHA256))
            {
                user.Salt = Convert.ToBase64String(hashedPassword.Salt);
                user.Password = Convert.ToBase64String(hashedPassword.GetBytes(hashLen));
            }
            await uow.UserRepository.InsertAsync(user);
            await uow.CommitAsync();
            return Login(new LoginDto { Email = registrationDTO.Email, Password = registrationDTO.Password });
        }

        private static bool VerifyPassword(string storedPassword, string storedSalt, string verifyPassword)
        {
            byte[] password = Convert.FromBase64String(storedPassword);
            byte[] salt = Convert.FromBase64String(storedSalt);
            using var hashedPassword = new Rfc2898DeriveBytes(verifyPassword, salt, iterations, HashAlgorithmName.SHA256);
            var inputPassword = hashedPassword.GetBytes(hashLen);
            return inputPassword.SequenceEqual(password);
        }

        private string GenerateToken(User user)
        {
            var secret = configuration["JWT:Secret"] ?? throw new Exception("Cannot get signing key");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Id.ToString()),
                new(ClaimTypes.GivenName, user.Name),
            };
            var issuer = configuration["Jwt:Issuer"] ?? throw new Exception("Cannot get issuer signing key");
            var token = new JwtSecurityToken(issuer, issuer, authClaims, null, DateTime.Now.AddYears(1), credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
