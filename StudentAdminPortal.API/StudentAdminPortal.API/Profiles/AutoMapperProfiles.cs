using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Profiles.AfterMaps;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Student, StudentDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();

            CreateMap<Gender, GenderDTO>().ReverseMap();

            CreateMap<UpdateStudentDTO,Student>().AfterMap<UpdateStudentRequestAfterMap>();

            CreateMap<AddStudentDTO, Student>().AfterMap<AddNewStudentAfterMap>();


        }





    }
}
