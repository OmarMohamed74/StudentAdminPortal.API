using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.Models
{
    public class StudentAdminContext:DbContext
    {
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options):base(options) { 
        
        }
        

        public DbSet<Student> Students { get;set; }
        public DbSet<Address> Address { get;set; }
        public DbSet<Gender> Gender { get;set; }

    }
}
