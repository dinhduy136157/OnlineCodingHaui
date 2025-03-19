using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace OnlineCodingHaui.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;

        public LessonController(ILessonService lessonService, IMapper mapper)
        {
            _lessonService = lessonService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var lesson = await _lessonService.GetAllLessonAsync();
            var lessonDto = _mapper.Map<IEnumerable<LessonDto>>(lesson);
            return Ok(lessonDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetLessonByIdAsync(int Id)
        {
            var lesson = await _lessonService.GetByIdAsync(Id);
            var lessonDto = _mapper.Map<LessonDto>(lesson);
            return Ok(lessonDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateLesson(LessonDto lessonDto)
        {
            var lesson = _mapper.Map<Lesson>(lessonDto);
            await _lessonService.AddLessonAsync(lesson);
            return Ok(lessonDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLesson(int id, LessonDto lessonDto)
        {
            if (id != lessonDto.LessonID)
                return BadRequest();

            var lesson = _mapper.Map<Lesson>(lessonDto);
            await _lessonService.UpdateLessonAsync(lesson);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLesson(int id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return Ok();
        }
    }
}
