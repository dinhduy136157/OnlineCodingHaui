using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;
using OnlineCodingHaui.Application.DTOs.Authentication;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        [HttpGet("me")]
        public async Task<ActionResult> GetCurrentStudent()
        {
            var studentIdClaim = User.FindFirst("StudentID")?.Value;
            if (string.IsNullOrEmpty(studentIdClaim))
            {
                return Unauthorized("Không tìm thấy ID trong token");
            }

            var student = await _studentService.GetCurrentStudentAsync(studentIdClaim);
            if (student == null) return NotFound("Không tìm thấy sinh viên");

            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(studentDto);
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var student = await _studentService.GetAllStudentAsync();
            var studentDto = _mapper.Map<IEnumerable<StudentDto>>(student);
            return Ok(studentDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudentByIdAsync(int Id)
        {
            var student = await _studentService.GetByIdAsync(Id);
            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(studentDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateStudent(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            await _studentService.AddStudentAsync(student);
            return Ok(studentDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, StudentDto studentDto)
        {
            if (id != studentDto.StudentID)
                return BadRequest();

            var student = _mapper.Map<Student>(studentDto);
            await _studentService.UpdateStudentAsync(student);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return Ok();
        }
    }
}
