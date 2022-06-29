using CarSharingApp.Dto;
using CarSharingApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CarSharingApp.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegistrationService(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public void Registartion(RegistrationDto dto)
        {
            var user = new User()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Login = dto.Login,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
            var hasshed = _passwordHasher.HashPassword(user, dto.Password);

            user.Password = hasshed;
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
