using OnlineCodingHaui.Application.DTOs.PistonAPI;
using OnlineCodingHaui.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCodingHaui.Application.Services.Implementations
{
    public class PistonApiService : IPistonApiService
    {
        private readonly HttpClient _httpClient;

        public PistonApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PistonResultDto> ExecuteAsync(PistonExecutionDto request)
        {
            var pistonRequest = new
            {
                language = request.Language,
                version = request.Version,
                files = new[]
                {
                    new { name = GetFileName(request.Language), content = request.Code }
                  },
                stdin = request.Stdin
            };

            var response = await _httpClient.PostAsJsonAsync("http://localhost:2000/api/v2/execute", pistonRequest);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return new PistonResultDto { Run = new PistonResultDto.RunResult { Stderr = $"Piston Error: {errorMsg}", Code = -1 } };
            }

            return await response.Content.ReadFromJsonAsync<PistonResultDto>();
        }

        // Hàm hỗ trợ đặt tên file cho đúng với ngôn ngữ
        private string GetFileName(string language)
        {
            return language switch
            {
                "csharp" => "main.cs",
                "cpp" => "main.cpp",
                "python" => "main.py",
                _ => "main.txt"
            };
        }

    }
}
