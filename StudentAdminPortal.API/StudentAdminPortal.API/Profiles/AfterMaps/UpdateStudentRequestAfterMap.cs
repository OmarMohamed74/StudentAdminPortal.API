using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentDTO, Student>
    {
        public void Process(UpdateStudentDTO source, Student destination, ResolutionContext context)
        {
            destination.Address = new Address
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
