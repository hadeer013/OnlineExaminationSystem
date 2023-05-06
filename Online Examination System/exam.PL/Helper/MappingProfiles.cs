using AutoMapper;
using exam.DAL.Entities;
using exam.DAL.Entities.Professor;
using exam.PL.Dtos;

namespace exam.PL.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddBaseEntityWithNameDto, Level>();
            CreateMap<AddBaseEntityWithNameDto, Subject>();
            CreateMap<AddBaseEntityWithNameDto, Department>();
            CreateMap<ApplicationUser, BaseApplicationUserDto>();
            CreateMap<SignUpDto, ProfessorSignUpRequests>();
            CreateMap<SignUpDto, BaseApplicationUserDto>();
            CreateMap<ProfessorSignUpRequests, ProfessorRequestsDto>();
        }
    }
}
