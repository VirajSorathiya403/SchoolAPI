using FluentValidation;
using SchoolAPI.Models;

namespace SchoolAPI.Validation
{
    public class SubjectModelValidator : AbstractValidator<SubjectModel>
    {
        public SubjectModelValidator()
        {
            RuleFor(subject => subject.SubjectName)
                .NotEmpty().WithMessage("Subject Name is required.")
                .Length(3, 100).WithMessage("Subject Name must be between 3 and 100 characters.");
            
            RuleFor(subject => subject.SubjectCode)
                .NotEmpty().WithMessage("Subject Code is required.")
                .Length(3, 10).WithMessage("Subject Code must be between 3 and 10 characters.");
            
            RuleFor(subject => subject.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}