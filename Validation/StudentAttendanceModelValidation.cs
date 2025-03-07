using FluentValidation;
using SchoolAPI.Models;

namespace SchoolAPI.Validation
{
    public class StudentAttendanceModelValidator : AbstractValidator<StudentAttendanceModel>
    {
        public StudentAttendanceModelValidator()
        {
            RuleFor(attendance => attendance.StudentId)
                .GreaterThan(0).WithMessage("Student Id is required and must be greater than 0.");

            RuleFor(attendance => attendance.SubjectId)
                .GreaterThan(0).WithMessage("Subject Id is required and must be greater than 0.");
            
            RuleFor(attendance => attendance.TeacherId)
                .GreaterThan(0).WithMessage("Teacher Id is required and must be greater than 0.");
            
            RuleFor(attendance => attendance.AttendanceDate)
                .NotEmpty().WithMessage("Attendance Date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Attendance Date cannot be in the future.");
            
            RuleFor(attendance => attendance.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => status == "Present" || status == "Absent" || status == "Late")
                .WithMessage("Status must be 'Present', 'Absent', or 'Late'.");
        }
    }
}