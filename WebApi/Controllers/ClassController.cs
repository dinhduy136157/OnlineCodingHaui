using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.Classes;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classesService;
        private readonly IMapper _mapper;

        public ClassController(IClassService classesService, IMapper mapper)
        {
            _classesService = classesService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var classes = await _classesService.GetAllClassAsync();
            var classesDto = _mapper.Map<IEnumerable<ClassDto>>(classes);
            return Ok(classesDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetClassByIdAsync(int Id)
        {
            var classes = await _classesService.GetByIdAsync(Id);
            var classesDto = _mapper.Map<ClassDto>(classes);
            return Ok(classesDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateClass(ClassDto classesDto)
        {
            var classes = _mapper.Map<Class>(classesDto);
            await _classesService.AddClassAsync(classes);
            return Ok(classesDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateClass(int id, ClassDto classesDto)
        {
            if (id != classesDto.ClassID)
                return BadRequest();

            var classes = _mapper.Map<Class>(classesDto);
            await _classesService.UpdateClassAsync(classes);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClass(int id)
        {
            await _classesService.DeleteClassAsync(id);
            return Ok();
        }
    }
}
