using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Authorize] // Yêu cầu xác thực
        public IActionResult Get()
        {
            return Ok(new { Message = "You are authenticated!" });
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")] // Yêu cầu role Admin
        public IActionResult GetAdmin()
        {
            return Ok(new { Message = "You are an admin!" });
        }
    }
}
