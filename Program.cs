using System.Reflection;
using FluentValidation.AspNetCore;
using SchoolAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

{
    builder.Services.AddControllers()
        .AddFluentValidation((c=>c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())));
}
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle    
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<StudentRepository>();
builder.Services.AddScoped<SubjectRepository>();
builder.Services.AddScoped<TeacherWiseSubjectRepository>();
builder.Services.AddScoped<StudentAttendanceRepository>();

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