using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly StudentAttendanceRepository _studentAttendanceRepository;

        public StudentAttendanceController(StudentAttendanceRepository studentAttendanceRepository)
        {
            _studentAttendanceRepository = studentAttendanceRepository;
        }
        
        #region Get All StudentAttendance
        [HttpGet("getallstudentAttendances")]
        public IActionResult GetAllStudentAttendances([FromQuery] DateTime? attendanceDate)
        {
            var studentAttendances = _studentAttendanceRepository.GetAllStudentAttendances(attendanceDate);
            return Ok(studentAttendances);
        }
        #endregion
        
        #region Get StudentAttendance By ID
        [HttpGet("{id}")]
        public IActionResult GetStudentAttendanceById(int id)
        {
            var studentAttendance = _studentAttendanceRepository.SelectByPk(id);
            if (studentAttendance == null)
            {
                return NotFound("StudentAttendance not found");
            }
            return Ok(studentAttendance);
        }
        #endregion
        
        #region Insert StudentAttendance
        [HttpPost("addstudentAttendance")]
        public IActionResult InsertStudentAttendance([FromBody] StudentAttendanceModel studentAttendance)
        {
            if (studentAttendance == null)
            {
                return BadRequest("Invalid studentAttendance data");
            }

            var isInserted = _studentAttendanceRepository.Insert(studentAttendance);
            if (isInserted)
            {
                return Ok(new { Message = "addstudentAttendance inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the studentAttendance");
        }
        #endregion
        
        #region Update StudentAttendance
        [HttpPut("update/{id}")]
        public IActionResult UpdateStudentAttendance(int id, [FromBody] StudentAttendanceModel studentAttendance)
        {
            if (studentAttendance == null || id != studentAttendance.AttendanceId)
            {
                return BadRequest("Invalid studentAttendance data or ID mismatch");
            }

            var isUpdated = _studentAttendanceRepository.Update(studentAttendance);
            if (!isUpdated)
            {
                return NotFound("StudentAttendance not found");
            }

            return NoContent();
        }
        #endregion
        
        #region Delete StudentAttendance
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteStudentAttendance(int id)
        {
            var isDeleted = _studentAttendanceRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("StudentAttendance not found");
            }
            return NoContent();
        }
        #endregion
        
        #region DropDown For GetTeachers
        [HttpGet("teachers")]
        public IActionResult GetTeachers()
        {
            var teachers = _studentAttendanceRepository.GetTeachers();
            if (!teachers.Any())
                return NotFound("No Teachers found!");

            return Ok(teachers);
        }
        #endregion
        
        #region DropDown For GetSubjects
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
            var subjects = _studentAttendanceRepository.GetSubjects();
            if (!subjects.Any())
                return NotFound("No Subjects found!");

            return Ok(subjects);
        }
        #endregion
        
        #region DropDown For GetStudents
        [HttpGet("students")]
        public IActionResult GetStudents()
        {
            var students = _studentAttendanceRepository.GetStudents();
            if (!students.Any())
                return NotFound("No Students found!");

            return Ok(students);
        }
        #endregion
    }
}
