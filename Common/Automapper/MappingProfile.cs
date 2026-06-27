using AutoMapper;
using Common.DTOs;
using DAL.Models;

namespace Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Registration, RegistrationDto>().ReverseMap();
            CreateMap<CreateRegistrationDto, Registration>();
            CreateMap<UpdateRegistrationDto, Registration>();
        }
    }
}
