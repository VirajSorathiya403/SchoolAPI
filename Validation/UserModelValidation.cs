using FluentValidation;
using SchoolAPI.Models;

namespace SchoolAPI.Validation;

public class UserModelValidation : AbstractValidator<UserModel>
{
    public UserModelValidation()
    {
        RuleFor(user => user.UserName)
            .NotEmpty().WithMessage("User Name is required.")
            .Length(3, 50).WithMessage("User Name must be between 3 and 50 characters.");
        
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        
        RuleFor(user => user.MobileNo)
            .NotEmpty().WithMessage("Mobile Number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Mobile Number must be valid.");
        
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address format.");
        
        RuleFor(user => user.UserRoleId)
            .GreaterThan(0).WithMessage("User Role is required and must be a positive number.");
        
        RuleFor(user => user.ContactPersonName)
            .NotEmpty().WithMessage("Contact Person Name is required.");
    }
}