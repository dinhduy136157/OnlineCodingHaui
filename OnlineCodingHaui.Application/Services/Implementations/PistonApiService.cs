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
            try
            {
                // Tạo yêu cầu gửi đến Piston API
                var pistonRequest = new
                {
                    language = request.Language,
                    version = request.Version,
                    files = new[] { new { name = GetFileName(request.Language), content = request.Code } },
                    stdin = request.Stdin
                };

                // Gửi yêu cầu đến Piston API
                var response = await _httpClient.PostAsJsonAsync("http://localhost:2000/api/v2/execute", pistonRequest);

                // Kiểm tra phản hồi từ Piston
                if (!response.IsSuccessStatusCode)
                {
                    // Nếu không thành công, lấy lỗi từ Piston và trả về
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error from Piston: {errorMsg}"); // Log lỗi chi tiết vào console (hoặc file log)

                    // Trả về lỗi chi tiết cho frontend
                    return new PistonResultDto
                    {
                        Run = new PistonResultDto.RunResult
                        {
                            Stderr = $"Piston Error: {errorMsg}",
                            Code = -1
                        }
                    };
                }

                // Nếu thành công, đọc kết quả trả về từ Piston
                var pistonResult = await response.Content.ReadFromJsonAsync<PistonResultDto>();

                // Kiểm tra lỗi biên dịch hoặc chạy từ Piston
                if (pistonResult?.Run?.Code != null)
                {
                    // Cố gắng chuyển đổi code về int, nếu không thành công thì gán -1
                    int code;
                    if (!int.TryParse(pistonResult.Run.Code.ToString(), out code))
                    {
                        code = -1; // Nếu không thể chuyển đổi, gán lỗi mặc định
                    }

                    if (code != 0)
                    {
                        // Nếu có lỗi biên dịch hoặc lỗi khi chạy, lấy stderr để trả về
                        Console.WriteLine($"Piston execution error: {pistonResult?.Run?.Stderr}");

                        return new PistonResultDto
                        {
                            Run = new PistonResultDto.RunResult
                            {
                                Stderr = pistonResult?.Run?.Stderr,
                                Code = code
                            }
                        };
                    }
                }
                else
                {
                    // Nếu không có code trả về, gán lỗi mặc định
                    return new PistonResultDto
                    {
                        Run = new PistonResultDto.RunResult
                        {
                            Stderr = "Unexpected response from Piston. No 'code' returned.",
                            Code = -1
                        }
                    };
                }

                // Trả về kết quả thành công từ Piston
                return pistonResult;
            }
            catch (Exception ex)
            {
                // Nếu có ngoại lệ xảy ra, log chi tiết lỗi và trả về thông báo lỗi cho sinh viên
                Console.WriteLine($"Exception occurred: {ex.Message}");

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



        // Hàm hỗ trợ đặt tên file cho đúng với ngôn ngữ
        private string GetFileName(string language)
        {
            return language switch
            {
                "csharp" => "main.cs",
                "cpp" => "main.cpp",
                "python" => "main.py",
                "java" => "main.java",
                _ => "main.txt"
            };
        }

    }
}
