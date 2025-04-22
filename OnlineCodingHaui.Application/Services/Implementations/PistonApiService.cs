using OnlineCodingHaui.Application.DTOs.PistonAPI;
using OnlineCodingHaui.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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
            try
            {
                // Tạo request payload với các tham số cần thiết
                var pistonRequest = new
                {
                    language = request.Language,
                    version = request.Version,
                    files = new[] { new { content = request.Code } },
                    stdin = request.Stdin,
                    compile_timeout = 3000, // Thêm timeout compile
                    run_timeout = 3000      // Thêm timeout run
                };

                // Gửi request đến Piston API
                var response = await _httpClient.PostAsJsonAsync("http://localhost:2000/api/v2/execute", pistonRequest);
                var rawJson = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Piston API Response: {rawJson}");

                if (!response.IsSuccessStatusCode)
                {
                    return new PistonResultDto
                    {
                        Run = new PistonResultDto.RunResult
                        {
                            Stderr = $"Piston API Error: {response.StatusCode} - {rawJson}",
                            Code = -1
                        }
                    };
                }

                // Phân tích response thủ công để tránh lỗi parse
                using var doc = JsonDocument.Parse(rawJson);
                var root = doc.RootElement;

                var result = new PistonResultDto
                {
                    Run = new PistonResultDto.RunResult()
                };

                // Xử lý phần compile (nếu có)
                if (root.TryGetProperty("compile", out var compile))
                {
                    if (compile.TryGetProperty("stderr", out var compileStderr) &&
                        !string.IsNullOrWhiteSpace(compileStderr.GetString()))
                    {
                        result.Run.Stderr = compileStderr.GetString();
                        result.Run.Code = -1;
                        return result;
                    }
                }

                // Xử lý phần run
                if (root.TryGetProperty("run", out var run))
                {
                    result.Run.Output = run.TryGetProperty("stdout", out var stdout)
                        ? stdout.GetString()
                        : string.Empty;

                    result.Run.Stderr = run.TryGetProperty("stderr", out var stderr)
                        ? stderr.GetString()
                        : string.Empty;

                    // Xử lý exit code linh hoạt
                    if (run.TryGetProperty("code", out var codeElement))
                    {
                        result.Run.Code = codeElement.ValueKind switch
                        {
                            JsonValueKind.Number => codeElement.GetInt32(),
                            JsonValueKind.String => int.TryParse(codeElement.GetString(), out var code) ? code : -1,
                            _ => string.IsNullOrEmpty(result.Run.Stderr) ? 0 : -1
                        };
                    }
                    else
                    {
                        result.Run.Code = string.IsNullOrEmpty(result.Run.Stderr) ? 0 : -1;
                    }
                }

                return result;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Parse Error: {jsonEx}");
                return new PistonResultDto
                {
                    Run = new PistonResultDto.RunResult
                    {
                        Stderr = $"Failed to parse Piston response: {jsonEx.Message}",
                        Code = -1
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex}");
                return new PistonResultDto
                {
                    Run = new PistonResultDto.RunResult
                    {
                        Stderr = $"Execution Error: {ex.Message}",
                        Code = -1
                    }
                };
            }
        }
    }
}