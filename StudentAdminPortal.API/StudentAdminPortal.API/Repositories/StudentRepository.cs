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

        public async Task<List<Gender>> GetGendersAsync()
        {
          return await _context.Gender.ToListAsync();
        }

        public async Task<bool> isStudentExsists(Guid studentId)
        {
            return await _context.Students.AnyAsync(x => x.Id == studentId);
        }

        public async Task<Student> UpdateStudentAsync(Guid studentId, Student std)
        {
            var currentStudent = await GetStudentByIdAsync(studentId);

            if(currentStudent != null)
            {
                currentStudent.FirstName = std.FirstName;
                currentStudent.LastName = std.LastName;
                currentStudent.DateOfBirth = std.DateOfBirth;
                currentStudent.Address.PhysicalAddress =std.Address.PhysicalAddress;
                currentStudent.GenderId= std.GenderId;
                currentStudent.Mobile = std.Mobile;
                currentStudent.Email= std.Email;

                await _context.SaveChangesAsync();

                return currentStudent;
            }
            return null;
        }

        public async Task<Student> DeleteStudentAsync(Guid studentId)
        {
            var student = await this.GetStudentByIdAsync(studentId);

            if(student != null)
            {
                      _context.Students.Remove(student);

                await _context.SaveChangesAsync();

                return student;
            }

            return null;
        }

        public async Task<Student> AddNewStudentAsync(Student newStudent)
        {
            var student =await  _context.Students.AddAsync(newStudent);
            await _context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<bool> UpdateImgProfile(Guid studentId, string imgProfile)
        {
            var student = await this.GetStudentByIdAsync(studentId);

            if(student != null)
            {
                student.ProfileImgUrl = imgProfile;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
