using CarSharingApp.Dto;

namespace CarSharingApp.Services
{
    public interface IRegistrationService
    {
        void Registartion(RegistrationDto dto);
        void RegistrationAdmin(RegistrationAdminDto dto);
        string GenerateJwt(LoginDto dto);
    }
}
