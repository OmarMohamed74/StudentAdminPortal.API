using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Student, StudentDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();

            CreateMap<Gender, GenderDTO>().ReverseMap();


        }





    }
}
