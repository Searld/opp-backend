using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OPP.Contracts.Projects;
using OPP.Contracts.ProjectsTask;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;
using OPP.Domain.Services;

namespace OPP.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : Controller
{
    private readonly ProjectsService _projectsService;
    private readonly GantaDbContext _context;

    public ProjectsController(ProjectsService projectsService,
        GantaDbContext context)
    {
        _projectsService = projectsService;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateProjectDto dto)
    {
        await _projectsService.CreateAsync(dto);
        return Ok();
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var project = await _context.Projects
            .Include(p=>p.Members)
            .Include(p=>p.ProjectTasks)
            .Include(p=>p.Subject)
            .FirstOrDefaultAsync(p=> p.Id == id);
        if(project == null)
            throw new NotFoundException("project", id);
        return Ok(new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.Deadline,
            project.ProjectTasks.Select(p=>p.Id).ToList(),
            project.Members.Select(p=>p.Id).ToList(),
            project.Subject.Name,
            project.CreatorId));
    }
    
    [HttpPost("task")]
    public async Task<IActionResult> AddTask(CreateProjectTaskDto dto)
    {
        await _projectsService.CreateProjectTaskAsync(dto);
        return Ok();
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _projectsService.GetAllProjectsAsync());
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _projectsService.DeleteProjectAsync(id);
        return Ok();
    }
}