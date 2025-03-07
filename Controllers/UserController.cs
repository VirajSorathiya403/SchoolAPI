using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        #region Get All Users
        [HttpGet("getallusers")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }
        #endregion
        
        #region Get User By ID
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.SelectByPk(id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
        #endregion
        
        #region Insert User
        [HttpPost("adduser")]
        public IActionResult InsertUser([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }

            var isInserted = _userRepository.Insert(user);
            if (isInserted)
            {
                return Ok(new { Message = "User inserted successfully" });
            }

            return StatusCode(500, "An error occurred while inserting the user");
        }
        #endregion
        
        #region Update User
        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel user)
        {
            if (user == null || id != user.UserId)
            {
                return BadRequest("Invalid user data or ID mismatch");
            }

            var isUpdated = _userRepository.Update(user);
            if (!isUpdated)
            {
                return NotFound("User not found");
            }
            
            return NoContent();
        }
        #endregion
            
        #region Delete User
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var isDeleted = _userRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound("User not found");
            }
            return NoContent();
        }
        #endregion

        #region DropDown For UserRoles
        [HttpGet("userroles")]
        public IActionResult GetUserRoles()
        {
            var userroles = _userRepository.GetUserRoles();
            if (!userroles.Any())
                return NotFound("No UserRoles found!");

            return Ok(userroles);
        }
        #endregion
        
        #region SignIn
        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] UserAuthModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                var result = _userRepository.SignInUser(user);
                if (result["Message"].ToString() == "Invalid Email or Password")
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion

        #region SignOut
        [HttpPost("signout")]
        public IActionResult SignOut([FromBody] UserAuthModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user data.");
            }

            try
            {
                var result = _userRepository.SignOutUser(user);
                if (result["Message"].ToString() == "Invalid Email or Password")
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        #endregion
    }
}
