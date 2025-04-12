using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCodingHaui.Application.DTOs.Authentication;
using OnlineCodingHaui.Application.DTOs.FunctionTemplate;
using OnlineCodingHaui.Application.Services.Implementations;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Domain.Entity;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionTemplateController : ControllerBase
    {
        private readonly IFunctionTemplateService _functionTemplateService;
        private readonly IMapper _mapper;

        public FunctionTemplateController(IFunctionTemplateService functionTemplateService, IMapper mapper)
        {
            _functionTemplateService = functionTemplateService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var functionTemplate = await _functionTemplateService.GetAllFunctionTemplateAsync();
            var functionTemplateDto = _mapper.Map<IEnumerable<FunctionTemplateDto>>(functionTemplate);
            return Ok(functionTemplateDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFunctionTemplateByIdAsync(int id)
        {
            var functionTemplate = await _functionTemplateService.GetByIdAsync(id);
            var functionTemplateDto = _mapper.Map<FunctionTemplateDto>(functionTemplate);
            return Ok(functionTemplateDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateFunctionTemplate(FunctionTemplateDto functionTemplateDto)
        {
            var functionTemplate = _mapper.Map<FunctionTemplate>(functionTemplateDto);
            await _functionTemplateService.AddFunctionTemplateAsync(functionTemplate);
            return Ok(functionTemplateDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFunctionTemplate(int id, FunctionTemplateDto functionTemplateDto)
        {
            if (id != functionTemplateDto.TemplateID)
                return BadRequest();

            var functionTemplate = _mapper.Map<FunctionTemplate>(functionTemplateDto);
            await _functionTemplateService.UpdateFunctionTemplateAsync(functionTemplate);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFunctionTemplate(int id)
        {
            await _functionTemplateService.DeleteFunctionTemplateAsync(id);
            return Ok();
        }
    }
}
