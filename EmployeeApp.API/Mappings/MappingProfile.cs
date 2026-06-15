using AutoMapper;
using EmployeeApp.API.Dto;
using EmployeeApp.API.Models;

namespace EmployeeApp.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
