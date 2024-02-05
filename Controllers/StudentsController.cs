using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //[HttpGet(Name = "GetNames")]
        //public String[] GetNames()
        //{
        //    return ["vinvitha", "vinay"];
        //}

        [HttpGet(Name = "GetStudents")]
        public IActionResult GetStudents()
        {
            string[] students = new string[] { "vinvitha", "vinay" };
            return Ok(students);
        }
    }
}
