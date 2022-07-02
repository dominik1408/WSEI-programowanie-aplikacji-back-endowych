using CarSharingApp.Dto;
using CarSharingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarSharingApp.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public RegistrationService(AppDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(a => a.Login == dto.Login);
            if (user is null)
            {
                return $"Invalid username or password";
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                return $"Invalid password";
            }

            var claism = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
                new Claim(ClaimTypes.Role, $"{user.Roles}"),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, 
                _authenticationSettings.JwtIssuer,
                claism,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void Registartion(RegistrationDto dto)
        {
            var user = new User()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Login = dto.Login,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Roles = Enums.Roles.User,
            };
            var hasshed = _passwordHasher.HashPassword(user, dto.Password);

            user.Password = hasshed;
            _context.Add(user);
            _context.SaveChanges();
        }

        public void RegistrationAdmin(RegistrationAdminDto dto)
        {
            var admin = new User()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Login = dto.Login,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Roles = Enums.Roles.Admin
            };
            var hasshed = _passwordHasher.HashPassword(admin, dto.Password);    

            admin.Password = hasshed;
            _context.Add(admin);
            _context.SaveChanges();
        }
    }
}
