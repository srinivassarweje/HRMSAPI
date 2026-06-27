using AutoMapper;
using Common.DTOs;
using DAL.Models;
using Repository.Interfaces;
using BLL.Services.Interfaces;

namespace BLL.Services.Implementation
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IMapper _mapper;

        public RegistrationService(IRegistrationRepository registrationRepository, IMapper mapper)
        {
            _registrationRepository = registrationRepository;
            _mapper = mapper;
        }

        public async Task<RegistrationDto?> GetRegistrationByIdAsync(int id)
        {
            var registration = await _registrationRepository.GetByIdAsync(id);
            return registration != null ? _mapper.Map<RegistrationDto>(registration) : null;
        }

        public async Task<IEnumerable<RegistrationDto>> GetAllRegistrationsAsync()
        {
            var registrations = await _registrationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RegistrationDto>>(registrations);
        }

        public async Task<IEnumerable<RegistrationDto>> GetActiveRegistrationsAsync()
        {
            var registrations = await _registrationRepository.GetActiveRegistrationsAsync();
            return _mapper.Map<IEnumerable<RegistrationDto>>(registrations);
        }

        public async Task<RegistrationDto?> GetRegistrationByEmailAsync(string email)
        {
            var registration = await _registrationRepository.GetByEmailAsync(email);
            return registration != null ? _mapper.Map<RegistrationDto>(registration) : null;
        }

        public async Task<RegistrationDto> CreateRegistrationAsync(CreateRegistrationDto createDto)
        {
            var registration = _mapper.Map<Registration>(createDto);
            registration.RegistrationDate = DateTime.UtcNow;
            registration.IsActive = true;

            var createdRegistration = await _registrationRepository.AddAsync(registration);
            return _mapper.Map<RegistrationDto>(createdRegistration);
        }

        public async Task<bool> UpdateRegistrationAsync(UpdateRegistrationDto updateDto)
        {
            var existingRegistration = await _registrationRepository.GetByIdAsync(updateDto.Id);
            if (existingRegistration == null)
                return false;

            var updatedRegistration = _mapper.Map(updateDto, existingRegistration);
            await _registrationRepository.UpdateAsync(updatedRegistration);
            return true;
        }

        public async Task<bool> DeleteRegistrationAsync(int id)
        {
            var registration = await _registrationRepository.GetByIdAsync(id);
            if (registration == null)
                return false;

            await _registrationRepository.DeleteAsync(registration);
            return true;
        }
    }
}
