using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Class02Homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET method to return all users
        [HttpGet] //http://localhost:[port]/api/users
        public ActionResult<List<string>> GetUser()
        {
            //return StatusCode(StatusCodes.Status200OK, StaticDb.userNames);
            return Ok(StaticDb.userNames);
        }

        // GET method to return one user (by index)
        [HttpGet("{index}")] //http://localhost:[port]/api/users/[index]
        public ActionResult<List<string>> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index has negative value");
                }
                
                if (index >= StaticDb.userNames.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.userNames[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator");
            }
        }

        // POST method that adds a new user
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string newUser = reader.ReadToEnd();

                    if (string.IsNullOrEmpty(newUser))
                    {
                        return BadRequest("The body of the request can not be empty");
                    }

                    StaticDb.userNames.Add(newUser);
                    return StatusCode(StatusCodes.Status201Created, "User added successfully");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred. Contact the administrator");
            }
        }
    }
}
