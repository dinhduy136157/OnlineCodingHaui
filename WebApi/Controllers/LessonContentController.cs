using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Lessons;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonContentController : ControllerBase
    {
        private readonly ILessonContentService _lessonContentService;
        private readonly IMapper _mapper;

        public LessonContentController(ILessonContentService lessonContentService, IMapper mapper)
        {
            _lessonContentService = lessonContentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var lessonContent = await _lessonContentService.GetAllLessonContentAsync();
            var lessonContentDto = _mapper.Map<IEnumerable<LessonContentDto>>(lessonContent);
            return Ok(lessonContentDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetLessonContentByIdAsync(int Id)
        {
            var lessonContent = await _lessonContentService.GetByIdAsync(Id);
            var lessonContentDto = _mapper.Map<LessonContentDto>(lessonContent);
            return Ok(lessonContentDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateLessonContent(LessonContentDto lessonContentDto)
        {
            var lessonContent = _mapper.Map<LessonContent>(lessonContentDto);
            await _lessonContentService.AddLessonContentAsync(lessonContent);
            return Ok(lessonContentDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLessonContent(int id, LessonContentDto lessonContentDto)
        {
            if (id != lessonContentDto.ContentID)
                return BadRequest();

            var lessonContent = _mapper.Map<LessonContent>(lessonContentDto);
            await _lessonContentService.UpdateLessonContentAsync(lessonContent);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLessonContent(int id)
        {
            await _lessonContentService.DeleteLessonContentAsync(id);
            return Ok();
        }
    }
}
