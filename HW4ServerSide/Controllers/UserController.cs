using HW4ServerSide.Models;
using HW4ServerSide.DAL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW4ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            DBservices db = new DBservices();
            return db.ReadUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public Users Get(int id)
        {
            List<Users> users = Users.Read();
            foreach(Users user in users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        // POST api/<UserController>
        [HttpPost("Register")]
        public ActionResult Register([FromBody] Users user)
        {
            DBservices dbs = new DBservices();
            try
            {
                int result = dbs.InsertUser(user);
                if (result > 0)
                {
                    return Ok(new { success = true, message = "User registered successfully!" });
                }
                else
                {
                    return BadRequest(new { success = true, message = "Failed to register user." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("Login")]
        public ActionResult<Users> Login([FromBody] Users loginUser)
        {
            DBservices db = new DBservices();
            List<Users> users = db.ReadUsers();

            foreach (Users user in users)
            {
                if (user.Email == loginUser.Email && Users.VerifyPassword(loginUser.Password, user.Password))
                {
                    return Ok(user);
                }
            }
            return Unauthorized("Invalid email or password");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Users user)
        {
            DBservices dbs = new DBservices();
            try
            {
                user.Id = id;
                int result = dbs.UpdateUser(user);
                if (result > 0)
                {
                    return Ok(new { success = true, message = $"The user with id {id} have been successfully updated!" });
                }
                else
                {
                    return NotFound(new { success = true, message = $"No user with Id {id} found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DBservices dbs = new DBservices();
            try
            {
                int result = dbs.DeleteUser(id);
                if (result > 0)
                {
                    return Ok($"User with Id {id} deleted successfully.");
                }
                else
                {
                    return NotFound($"No user with Id {id} found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}