using FluentValidation;
using SchoolAPI.Models;

namespace SchoolAPI.Validation
{
    public class StudentModelValidator : AbstractValidator<StudentModel>
    {
        public StudentModelValidator()
        {
            RuleFor(student => student.StudentName)
                .NotEmpty().WithMessage("Student Name is required.")
                .Length(3, 100).WithMessage("Student Name must be between 3 and 100 characters.");
            
            RuleFor(student => student.MobileNo)
                .NotEmpty().WithMessage("Mobile Number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Mobile Number must be valid.");
            
            RuleFor(student => student.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.");
            
            RuleFor(student => student.ParentNo)
                .NotEmpty().WithMessage("Parent Mobile Number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Parent Mobile Number must be valid.");
            
            RuleFor(student => student.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of Birth cannot be in the future.");
            
            RuleFor(student => student.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => g == "Male" || g == "Female" || g == "Other").WithMessage("Gender must be Male, Female, or Other.");
            
            RuleFor(student => student.EnrollmentNumber)
                .GreaterThan(0).WithMessage("Enrollment Number is required and must be greater than 0.");
        }
    }
}