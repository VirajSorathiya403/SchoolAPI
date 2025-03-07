using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models
{
    public class StudentAttendanceModel
    {
        public int? AttendanceId { get; set; }
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? Email { get; set; }
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        [DataType(DataType.Date)]
        public DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
    }
    
    public class StudentsDropDownModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}