using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;

        public StudentController(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        
        #region Get All Students
        [HttpGet("getallstudents")]
        public IActionResult GetAllTeachers()
        {
            var students = _studentRepository.GetAllStudents();
            return Ok(students);
        }
        #endregion
        
        #region Get Student By ID
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var student = _studentRepository.SelectByPk(id);
            if (student == null)
            {
                return NotFound("Teacher not found");
            }
            return Ok(student);
        }
        #endregion
        
        #region Insert Student
        [HttpPost("addstudent")]
        public IActionResult InsertTeacher([FromBody] StudentModel student)
        {
            if (student == null)
            {
                return BadRequest("Invalid student data");
            }

            var isInserted = _studentRepository.Insert(student);
            if (isInserted)
            {
                return Ok(new { Message = "Student inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the student");
        }
        #endregion
        
        #region Update Student
        [HttpPut("update/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentModel student)
        {
            if (student == null || id != student.StudentId)
            {
                return BadRequest("Invalid teacher data or ID mismatch");
            }

            var isUpdated = _studentRepository.Update(student);
            if (!isUpdated)
            {
                return NotFound("Student not found");
            }

            return NoContent();
        }
        #endregion
        
        #region Delete Student
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var isDeleted = _studentRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Student not found");
            }
            return NoContent();
        }
        #endregion
    }
}
