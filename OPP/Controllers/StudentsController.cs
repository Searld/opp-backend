using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPP.Contracts.Student;
using OPP.Domain.Features.Students;
using OPP.Domain.Services;

namespace OPP.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterStudentDto dto,
        [FromServices] RegisterStudentUseCase useCase)
    {
        await useCase.Execute(dto);
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginStudentDto dto,
        [FromServices] LoginStudentUseCase useCase)
    {
        var sessionId = await useCase.Execute(dto);
        var expires = DateTime.UtcNow.AddMinutes(10);
        var cookieValue = $"session={sessionId}; HttpOnly; Path=/; Expires={expires:R}"; 
        Response.Headers.Append("Set-Cookie", cookieValue);
        return Ok();
    }

    [HttpGet("get/{studentId}")]
    public async Task<IActionResult> Get(Guid studentId, [FromServices] GetStudentByIdUseCase useCase)
    {
        return Ok(await useCase.Execute(studentId));
    }
    
    [HttpGet("get/email/{email}")]
    public async Task<IActionResult> Get(string email, [FromServices] GetStudentByEmailUseCase useCase)
    {
        return Ok(await useCase.Execute(email));
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> Get(
        [FromServices] GetAllStudentsUseCase useCase)
    {
        return Ok(await useCase.ExecuteAsync());
    }

    [Authorize]
    [HttpPost("invite")]
    public async Task<IActionResult> InviteStudentInProject(
        InviteStudentDto dto,
        [FromServices] InviteStudentUseCase useCase)
    {
        var studentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await useCase.Execute(studentId, dto);
        return Ok();
    }
    
    [Authorize]
    [HttpGet("accept-invite/{studentId}/{projectId}")]
    public async Task<IActionResult> AcceptInviteStudentInProject(
        Guid studentId, Guid projectId,
        [FromServices] AcceptInviteInProjectUseCase useCase)
    {
        var currentStudentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if(currentStudentId != studentId)
            return Forbid();
        await useCase.Execute(studentId, projectId);
        return Ok("Вы успешно добавлены в проект, можете закрыть вкладку");
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me(
        [FromServices] GetStudentUseCase useCase)
    {
        var currentStudentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        return Ok(await useCase.Execute(currentStudentId));
    }
    
    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteFromProject(
        [FromServices] DeleteStudentFromProjectUseCase useCase,
        [FromBody] DeleteStudentFromProjectDto dto)
    {
        var currentStudentId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        if(currentStudentId != dto.DeletedById)
            return Forbid();
        await useCase.Execute(dto);
        return Ok();
    }
}