using ApiBoilerPlate.Infrastructure.Helpers;
using FluentValidation;
using System;

namespace ApiBoilerPlate.DTO.Request
{
    public class UpdatePersonRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class UpdatePersonRequestValidator : AbstractValidator<UpdatePersonRequest>
    {
        public UpdatePersonRequestValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty();
            RuleFor(o => o.LastName).NotEmpty();
            RuleFor(o => o.DateOfBirth)
                    .NotEmpty()
                    .Must(PropertyValidation.IsValidDateTime)
                    .LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");
        }
    }
}
