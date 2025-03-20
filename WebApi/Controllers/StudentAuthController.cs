using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentService;
        public StudentAuthController(IConfiguration configuration, IStudentService studentService)
        {
            _configuration = configuration;
            _studentService = studentService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] StudentLoginDto studentDto)
        {
            var student = await _studentService.AuthenticateStudentAsync(studentDto.StudentID, studentDto.Password);

            if (student == null)
            {
                return Unauthorized(new { Message = "Invalid StudentID or Password" });
            }

            // Tạo token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, student.StudentID.ToString()),
            new Claim(ClaimTypes.Name, student.FirstName + student.LastName),
            new Claim(ClaimTypes.Role, "Student")
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

    }
}
