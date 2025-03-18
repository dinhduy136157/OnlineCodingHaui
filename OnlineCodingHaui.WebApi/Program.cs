using Microsoft.EntityFrameworkCore;
using OnlineCodingHaui.Application.Services.Implementations;
using OnlineCodingHaui.Application.Services.Interfaces;
using OnlineCodingHaui.Infrastructure.Context;
using OnlineCodingHaui.Infrastructure.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OnlineCodingHauiContext>(ops => ops.UseSqlServer(builder.Configuration.GetConnectionString("OnlineCodingHauiConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IStudentService, StudentService>();

//auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
