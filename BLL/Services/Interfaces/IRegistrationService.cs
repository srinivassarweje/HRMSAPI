using Common.DTOs;

namespace BLL.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationDto?> GetRegistrationByIdAsync(int id);
        Task<IEnumerable<RegistrationDto>> GetAllRegistrationsAsync();
        Task<IEnumerable<RegistrationDto>> GetActiveRegistrationsAsync();
        Task<RegistrationDto?> GetRegistrationByEmailAsync(string email);
        Task<RegistrationDto> CreateRegistrationAsync(CreateRegistrationDto createDto);
        Task<bool> UpdateRegistrationAsync(UpdateRegistrationDto updateDto);
        Task<bool> DeleteRegistrationAsync(int id);
    }
}
