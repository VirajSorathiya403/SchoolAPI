using FluentValidation;
using SchoolAPI.Models;

namespace SchoolAPI.Validation
{
    public class TeacherWiseSubjectModelValidator : AbstractValidator<TeacherWiseSubjectModel>
    {
        public TeacherWiseSubjectModelValidator()
        {
            RuleFor(model => model.TeacherId)
                .GreaterThan(0).WithMessage("Teacher Id is required and must be greater than 0.");
            
            RuleFor(model => model.SubjectId)
                .GreaterThan(0).WithMessage("Subject Id is required and must be greater than 0.");
            
            RuleFor(model => model.AcademicYearId)
                .GreaterThan(0).WithMessage("Academic Year Id is required and must be greater than 0.");
        }
    }
}