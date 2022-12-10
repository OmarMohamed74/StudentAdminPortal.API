using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentAdminContext _context;

        public StudentRepository(StudentAdminContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(Guid studentId)
        {
            return await _context.Students.Include(x=>x.Gender).Include(x=>x.Address)
                                          .FirstOrDefaultAsync(x => x.Id == studentId);
        }
    }
}
