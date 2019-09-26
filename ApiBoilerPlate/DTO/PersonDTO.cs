using FluentValidation;
using System;

namespace ApiBoilerPlate.DTO
{
    public class PersonDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty();
            RuleFor(o => o.LastName).NotEmpty();
            RuleFor(o => o.DateOfBirth).NotEmpty();
        }
    }
}


