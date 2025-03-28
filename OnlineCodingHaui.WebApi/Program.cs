﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineCodingHaui.Application.Services.Implementations;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Infrastructure.Context;
using OnlineCodingHaui.Infrastructure.UnitOfWorks;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins", policy =>
        policy.WithOrigins("http://localhost:3000") // Chỉ cho phép React truy cập
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddDbContext<OnlineCodingHauiContext>(ops => ops.UseSqlServer(builder.Configuration.GetConnectionString("OnlineCodingHauiConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<IClassStudentService, ClassStudentService>();
builder.Services.AddTransient<ICodingExerciseService, CodingExerciseService>();
builder.Services.AddTransient<ILessonContentService, LessonContentService>();
builder.Services.AddTransient<ILessonService, LessonService>();
builder.Services.AddTransient<ISubjectService, SubjectService>();
builder.Services.AddTransient<ISubmissionService, SubmissionService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<ITestCaseService, TestCaseService>();



//auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Cấu hình JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("jwt key missing"));

if (key.Length < 32)
{
    throw new InvalidOperationException("Key must be 32 characters");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        // Hiển thị chi tiết lỗi khi xác thực token thất bại
        options.IncludeErrorDetails = true;
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Api",
        Version = "v1",
        Description = "API authentication with Jwt"
    });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Input token"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type  = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             },
             new string[]{}
         }
     });
});


var app = builder.Build();

// Sử dụng CORS
app.UseCors("_myAllowSpecificOrigins");

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();
app.Run();
