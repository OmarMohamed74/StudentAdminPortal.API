using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.DomainModels
{
    public class StudentDTO
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public string? ProfileImgUrl { get; set; }
        public Guid GenderId { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }



    }
}
