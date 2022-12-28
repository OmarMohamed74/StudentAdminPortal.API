using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Repositories;
using StudentAdminPortal.API.Repositories.Interfaces;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository ManageStudents;
        private readonly IImageRepository ManageImage;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository manageStudents,IImageRepository manageImage,IMapper mapper)
        {
            ManageStudents = manageStudents;
            ManageImage = manageImage;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
          var students= await ManageStudents.GetAllStudentsAsync();

          return Ok(_mapper.Map<List<StudentDTO>>(students));  
        }


        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]

        public async Task<IActionResult> GetStudentAsync([FromRoute]Guid studentId)
        {
            var student = await ManageStudents.GetStudentByIdAsync(studentId);

            if(student == null)
            {
                return NotFound(); 
            }

            return Ok(_mapper.Map<StudentDTO>(student));
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync(Guid studentId)
        {
            if (await ManageStudents.isStudentExsists(studentId))
            {
                // return the deleted student 
                var student = await ManageStudents.DeleteStudentAsync(studentId);

                return Ok(_mapper.Map<StudentDTO>(student));

            }
            return NotFound();

        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]

        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentDTO updateStudentRequest)
        {

            if (await ManageStudents.isStudentExsists(studentId))
            {
                var updateStudent = await ManageStudents.UpdateStudentAsync(studentId, _mapper.Map<Student>(updateStudentRequest));
                    
                if (updateStudent != null)
                {
                    return Ok(_mapper.Map<Student>(updateStudent));
                }
            }
            return NotFound();

        }
        [HttpPost]
        [Route("[controller]/AddNewStudent")]
        
        public async Task<IActionResult> AddNewStudentAsync([FromBody] AddStudentDTO newStudent){

            var student = await ManageStudents.AddNewStudentAsync(_mapper.Map<Student>(newStudent));

            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },
                _mapper.Map<Student>(student));
            
        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/uploadStdImg")]

        public async Task<IActionResult> UploadStdProfileImg([FromRoute] Guid studentId,IFormFile stdImgFile)
        {

            var validExtensionsList = new List<string> { ".jpeg", ".png", ".jpg",".gif" };

            var imgExtension= Path.GetExtension(stdImgFile.FileName);

            if (stdImgFile !=null && stdImgFile.Length > 0) {

                if (validExtensionsList.Contains(imgExtension))
                {

                    if (await ManageStudents.isStudentExsists(studentId))
                      {
                        var student = await ManageStudents.GetStudentByIdAsync(studentId);

                        string fileName = student.FirstName + Path.GetExtension(stdImgFile.FileName);

                        var fileImgPath = await ManageImage.Upload(stdImgFile, fileName);

                        if (await ManageStudents.UpdateImgProfile(studentId, fileImgPath))
                        {
                            return Ok(fileImgPath);
                        }
                        return StatusCode(StatusCodes.Status500InternalServerError, "Fialed to upload image");

                    }
                }
                return BadRequest("Invalid Img Format");
            }

            return NotFound();
        }
























        // Without AutoMapper

        //var domainModelsStudents = new List<StudentDTO>();

        //foreach(var student in students)
        //{
        //    domainModelsStudents.Add(new StudentDTO()
        //    {
        //        Id = student.Id,
        //        FirstName = student.FirstName,
        //        LastName = student.LastName,
        //        DateOfBirth = student.DateOfBirth,
        //        Email = student.Email,
        //        Mobile = student.Mobile,
        //        ProfileImgUrl = student.ProfileImgUrl,
        //        GenderId = student.GenderId,
        //        Address = new Address()
        //        {
        //            Id=student.Address.Id,
        //            PhysicalAddress = student.Address.PhysicalAddress,
        //            PostalAddress = student.Address.PostalAddress,
        //        },
        //        Gender= new Gender()
        //        {
        //            Id=student.Gender.Id,
        //            Description=student.Gender.Description,
        //        }     
        //    });
        //}



    }
}
