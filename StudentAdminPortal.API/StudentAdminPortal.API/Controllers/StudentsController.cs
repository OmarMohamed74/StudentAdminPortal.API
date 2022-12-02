﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Models;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository ManageStudents;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository manageStudents,IMapper mapper)
        {
            ManageStudents = manageStudents;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
          var students= await ManageStudents.GetAllStudentsAsync();

          return Ok(_mapper.Map<List<StudentDTO>>(students));
            
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
}