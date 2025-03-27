using Microsoft.AspNetCore.Authentication.JwtBearer;
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

//Cấu hình piston api
builder.Services.AddHttpClient("Piston", client =>
{
    client.BaseAddress = new Uri("http://piston-server:2000");
    client.Timeout = TimeSpan.FromSeconds(30);
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
builder.Services.AddScoped<IPistonApiService, PistonApiService>();



//auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Thêm dịch vụ authentication và JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Kích hoạt authentication và authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();