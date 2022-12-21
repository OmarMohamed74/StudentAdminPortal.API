using StudentAdminPortal.API.Models;

namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        // Student Signatures

        Task< List<Student>> GetAllStudentsAsync();

        Task<Student> GetStudentByIdAsync(Guid studentId);

        Task<Student> UpdateStudentAsync(Guid studentId, Student std);

        Task<bool> isStudentExsists(Guid studentId);

        Task<Student> DeleteStudentAsync(Guid studentId);

        Task<Student> AddNewStudentAsync(Student newStudent);

        Task<bool> UpdateImgProfile(Guid studentId,string imgProfile);

        //Gender Signatures
        Task<List<Gender>> GetGendersAsync();




    }
}
