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
    public class TeacherAuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITeacherService _teacherService;
        public TeacherAuthController(IConfiguration configuration, ITeacherService teacherService)
        {
            _configuration = configuration;
            _teacherService = teacherService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TeacherLoginDto teacherDto)
        {
            var teacher = await _teacherService.AuthenticateTeacherAsync(teacherDto.Email, teacherDto.Password);

            if (teacher == null)
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
            new Claim("TeacherID", teacher.TeacherID.ToString()), // Sửa lỗi này
            new Claim(ClaimTypes.Name, $"{teacher.FirstName} {teacher.LastName}"), // Sửa lỗi này
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
