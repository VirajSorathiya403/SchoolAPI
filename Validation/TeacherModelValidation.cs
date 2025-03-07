using FluentValidation;
using SchoolAPI.Models;

namespace SchoolAPI.Validation;

public class TeacherModelValidator : AbstractValidator<TeacherModel>
{
    public TeacherModelValidator()
    {
        RuleFor(teacher => teacher.TeacherName)
            .NotEmpty().WithMessage("Teacher Name is required.")
            .Length(3, 100).WithMessage("Teacher Name must be between 3 and 100 characters.");
        
        RuleFor(teacher => teacher.MobileNo)
            .NotEmpty().WithMessage("Mobile Number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Mobile Number must be valid.");
        
        RuleFor(teacher => teacher.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address format.");
        
        RuleFor(teacher => teacher.SchoolId)
            .GreaterThan(0).WithMessage("School Id is required and must be greater than 0.");
        
        RuleFor(teacher => teacher.DateOfBirth)
            .NotEmpty().WithMessage("Date of Birth is required.")
            .LessThan(DateTime.Now).WithMessage("Date of Birth cannot be in the future.");

        RuleFor(teacher => teacher.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than 0.");
        
        RuleFor(teacher => teacher.ExperienceYears)
            .GreaterThanOrEqualTo(0).WithMessage("Experience years cannot be negative.");
        
        RuleFor(teacher => teacher.JoiningDate)
            .NotEmpty().When(teacher => teacher.TeacherId.HasValue).WithMessage("Joining Date is required when Teacher Id is provided.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Joining Date cannot be in the future.");
        
        RuleFor(teacher => teacher.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(g => g == "Male" || g == "Female" || g == "Other").WithMessage("Gender must be Male, Female, or Other.");
    }
}