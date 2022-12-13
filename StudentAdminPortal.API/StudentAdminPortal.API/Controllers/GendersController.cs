using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IStudentRepository ManageStudents;
        private readonly IMapper _mapper;

        public GendersController(IStudentRepository manageStudents, IMapper mapper)
        {
            ManageStudents = manageStudents;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("[controller]")]
        // controller/action -- gender/GetAllGenders
        public async Task<IActionResult> GetAllGenders()
        {
            List<Gender> genders = await ManageStudents.GetGendersAsync();

            if(genders==null || !genders.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<GenderDTO>>(genders));

        }
    }
}
