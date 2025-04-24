using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.Services.Implementations;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService lessonContentService, IMapper mapper)
        {
            _teacherService = lessonContentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var lessonContent = await _teacherService.GetAllTeacherAsync();
            var teacher = _mapper.Map<IEnumerable<TeacherDto>>(lessonContent);
            return Ok(teacher);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeacherByIdAsync(int id)
        {
            var lessonContent = await _teacherService.GetByIdAsync(id);
            var teacher = _mapper.Map<TeacherDto>(lessonContent);
            return Ok(teacher);
        }
        [HttpPost]
        public async Task<ActionResult> CreateTeacher(TeacherDto teacher)
        {
            var lessonContent = _mapper.Map<Teacher>(teacher);
            await _teacherService.AddTeacherAsync(lessonContent);
            return Ok(teacher);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeacher(int id, TeacherDto teacher)
        {
            if (id != teacher.TeacherID)
                return BadRequest();

            var lessonContent = _mapper.Map<Teacher>(teacher);
            await _teacherService.UpdateTeacherAsync(lessonContent);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            await _teacherService.DeleteTeacherAsync(id);
            return Ok();
        }
        [HttpGet("me")]
        public async Task<ActionResult> GetCurrentTeacher()
        {
            var teacherIdClaim = User.FindFirst("TeacherID")?.Value;
            if (string.IsNullOrEmpty(teacherIdClaim))
            {
                return Unauthorized("Không tìm thấy ID trong token");
            }

            var teacher = await _teacherService.GetCurrentTeacherAsync(teacherIdClaim);
            if (teacher == null) return NotFound("Không tìm thấy giảng viên");

            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            return Ok(teacherDto);
        }
    }
}
