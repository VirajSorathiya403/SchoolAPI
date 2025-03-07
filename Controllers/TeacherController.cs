using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherRepository _teacherRepository;

        public TeacherController(TeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        
        #region Get All Teachers
        [HttpGet("getallteachers")]
        public IActionResult GetAllTeachers()
        {
            var teachers = _teacherRepository.GetAllTeachers();
            return Ok(teachers);
        }
        #endregion
        
        #region Get Teacher By ID
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _teacherRepository.SelectByPk(id);
            if (teacher == null)
            {
                return NotFound("Teacher not found");
            }
            return Ok(teacher);
        }
        #endregion
        
        #region Insert Teacher
        [HttpPost("addteacher")]
        public IActionResult InsertTeacher([FromBody] TeacherModel teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Invalid teacher data");
            }

            var isInserted = _teacherRepository.Insert(teacher);
            if (isInserted)
            {
                return Ok(new { Message = "Teacher inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the teacher");
        }
        #endregion
        
        #region Update Teacher
        [HttpPut("update/{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] TeacherModel teacher)
        {
            if (teacher == null || id != teacher.TeacherId)
            {
                return BadRequest("Invalid teacher data or ID mismatch");
            }

            var isUpdated = _teacherRepository.Update(teacher);
            if (!isUpdated)
            {
                return NotFound("Teacher not found");
            }

            return NoContent();
        }
        #endregion
        
        #region Delete Teacher
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var isDeleted = _teacherRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Teacher not found");
            }
            return NoContent();
        }
        #endregion
        
        #region DropDown For School
        [HttpGet("schools")]
        public IActionResult GetSchools()
        {
            var schools = _teacherRepository.GetSchools();
            if (!schools.Any())
                return NotFound("No Schools found!");

            return Ok(schools);
        }
        #endregion
    }
}
