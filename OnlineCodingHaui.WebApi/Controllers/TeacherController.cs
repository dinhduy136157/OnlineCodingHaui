using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace OnlineCodingHaui.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _lessonContentService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService lessonContentService, IMapper mapper)
        {
            _lessonContentService = lessonContentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var lessonContent = await _lessonContentService.GetAllTeacherAsync();
            var teacher = _mapper.Map<IEnumerable<TeacherDto>>(lessonContent);
            return Ok(teacher);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeacherByIdAsync(int Id)
        {
            var lessonContent = await _lessonContentService.GetByIdAsync(Id);
            var teacher = _mapper.Map<TeacherDto>(lessonContent);
            return Ok(teacher);
        }
        [HttpPost]
        public async Task<ActionResult> CreateTeacher(TeacherDto teacher)
        {
            var lessonContent = _mapper.Map<Teacher>(teacher);
            await _lessonContentService.AddTeacherAsync(lessonContent);
            return Ok(teacher);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeacher(int id, TeacherDto teacher)
        {
            if (id != teacher.TeacherID)
                return BadRequest();

            var lessonContent = _mapper.Map<Teacher>(teacher);
            await _lessonContentService.UpdateTeacherAsync(lessonContent);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            await _lessonContentService.DeleteTeacherAsync(id);
            return Ok();
        }
    }
}
