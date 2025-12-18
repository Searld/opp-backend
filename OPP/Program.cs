using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using OPP.Application;
using OPP.Domain.Data;
using OPP.Domain.Features.Students;
using OPP.Domain.Services;
using OPP.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISessionService,SessionsService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000", "http://192.168.68.109:3000", "http://backend.local:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddAuthentication("Session")
    .AddScheme<AuthenticationSchemeOptions, SessionAuthenticationHandler>("Session", options => { });

builder.Services.AddAuthorization();

builder.Services
    .AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<InviteStudentUseCase>();
builder.Services.AddScoped<LoginStudentUseCase>();
builder.Services.AddScoped<AcceptInviteInProjectUseCase>();
builder.Services.AddScoped<RegisterStudentUseCase>();
builder.Services.AddScoped<GetAllStudentsUseCase>();
builder.Services.AddScoped<GetStudentByIdUseCase>();
builder.Services.AddScoped<GetStudentByEmailUseCase>();
builder.Services.AddScoped<DeleteStudentFromProjectUseCase>();
builder.Services.AddScoped<GetStudentUseCase>();
builder.Services.AddScoped<SubjectsService>();
builder.Services.AddScoped<ProjectsService>();
builder.Services.AddScoped<ProjectTasksService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddDbContext<GantaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DebugConnection")));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors("AllowAll"); 

app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllers();
app.Run("http://0.0.0.0:5000");
