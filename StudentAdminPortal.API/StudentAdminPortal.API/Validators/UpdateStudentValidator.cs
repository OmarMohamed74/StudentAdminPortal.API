using FluentValidation;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Validators
{
    public class UpdateStudentValidator:AbstractValidator<UpdateStudentDTO>
    {
        public UpdateStudentValidator(IStudentRepository studentRepository)
        {

            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.Mobile).GreaterThan(8888).LessThan(9000000000);

            //selectedList gender 

            RuleFor(x => x.GenderId).NotEmpty().Must(genderId =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x => x.Id == genderId);

                if (gender != null)
                {
                    return true;
                }

                return false;

            }).WithMessage("Not Valid Gender");




        }
    }
}

