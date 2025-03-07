using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectRepository _subjectrepository;

        public SubjectController(SubjectRepository subjectrepository)
        {
            _subjectrepository = subjectrepository;
        }
        
        #region Get All Subjects
        [HttpGet("getallsubjects")]
        public IActionResult GetAllSubjects()
        {
            var subjects = _subjectrepository.GetAllSubjects();
            return Ok(subjects);
        }
        #endregion
        
        #region Get Subject By ID
        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            var subject = _subjectrepository.SelectByPk(id);
            if (subject == null)
            {
                return NotFound("Subject not found");
            }
            return Ok(subject);
        }
        #endregion
        
        #region Insert Subject
        [HttpPost("addsubject")]
        public IActionResult InsertTeacher([FromBody] SubjectModel subject)
        {
            if (subject == null)
            {
                return BadRequest("Invalid subject data");
            }

            var isInserted = _subjectrepository.Insert(subject);
            if (isInserted)
            {
                return Ok(new { Message = "Subject inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the subject");
        }
        #endregion
        
        #region Update Subject
        [HttpPut("update/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] SubjectModel subject)
        {
            if (subject == null || id != subject.SubjectId)
            {
                return BadRequest("Invalid subject data or ID mismatch");
            }

            var isUpdated = _subjectrepository.Update(subject);
            if (!isUpdated)
            {
                return NotFound("Student not found");
            }

            return NoContent();
        }
        #endregion
        
        #region Delete Subject
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteSubject(int id)
        {
            var isDeleted = _subjectrepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("Subject not found");
            }
            return NoContent();
        }
        #endregion
    }
}
