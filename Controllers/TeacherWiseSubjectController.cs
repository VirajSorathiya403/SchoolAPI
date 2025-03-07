using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherWiseSubjectController : ControllerBase
    {
        private readonly TeacherWiseSubjectRepository _teacherWiseSubjectRepository;

        public TeacherWiseSubjectController(TeacherWiseSubjectRepository teacherWiseSubjectRepository)
        {
            _teacherWiseSubjectRepository = teacherWiseSubjectRepository;
        }
        
        #region Get All TeacherWiseSubject
        [HttpGet("getallteacherWiseSubjects")]
        public IActionResult GetAllUsers()
        {
            var teacherWiseSubjects = _teacherWiseSubjectRepository.GetAllTeacherWiseSubject();
            return Ok(teacherWiseSubjects);
        }
        #endregion
        
        #region Insert TeacherWiseSubject
        [HttpPost("addteacherWiseSubject")]
        public IActionResult InsertTeacherWiseSubject([FromBody] TeacherWiseSubjectModel teacherWiseSubject)
        {
            if (teacherWiseSubject == null)
            {
                return BadRequest("Invalid teacherWiseSubject data");
            }

            var isInserted = _teacherWiseSubjectRepository.Insert(teacherWiseSubject);
            if (isInserted)
            {
                return Ok(new { Message = "TeacherWiseSubject inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the teacherWiseSubject");
        }
        #endregion
        
        #region Delete TeacherWiseSubject
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteTeacherWiseSubject(int id)
        {
            var isDeleted = _teacherWiseSubjectRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("TeacherWiseSubject not found");
            }
            return NoContent();
        }
        #endregion
        
        #region DropDown For GetTeachers
        [HttpGet("teachers")]
        public IActionResult GetTeachers()
        {
            var teachers = _teacherWiseSubjectRepository.GetTeachers();
            if (!teachers.Any())
                return NotFound("No Teachers found!");

            return Ok(teachers);
        }
        #endregion
        
        #region DropDown For GetSubjects
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
            var subjects = _teacherWiseSubjectRepository.GetSubjects();
            if (!subjects.Any())
                return NotFound("No Subjects found!");

            return Ok(subjects);
        }
        #endregion
        
        #region DropDown For GetAcademicYears
        [HttpGet("academicYears")]
        public IActionResult GetAcademicYears()
        {
            var academicYears = _teacherWiseSubjectRepository.GetAcademicYears();
            if (!academicYears.Any())
                return NotFound("No AcademicYears found!");

            return Ok(academicYears);
        }
        #endregion
    }
}
