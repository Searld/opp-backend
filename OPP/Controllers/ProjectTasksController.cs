using Microsoft.AspNetCore.Mvc;
using OPP.Contracts.ProjectsTask;
using OPP.Domain.Services;

namespace OPP.Controllers;

[ApiController]
[Route("api/project-tasks")]
public class ProjectTasksController : Controller
{
    private readonly ProjectTasksService _projectTasksService;

    public ProjectTasksController(ProjectTasksService projectTasksService)
    {
        _projectTasksService = projectTasksService;
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetAllTasks(Guid projectId)
    {
        return Ok(await _projectTasksService.GetAllProjectTasks(projectId));
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> ChangeTaskStatus(Guid taskId)
    {
        await  _projectTasksService.ChangeTaskStatus(taskId);
        return Ok();
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        await _projectTasksService.DeleteTask(taskId);
        return Ok();
    }
    [HttpPatch]
    public async Task<IActionResult> UpdateTask(EditProjectTaskDto dto)
    {
        await _projectTasksService.UpdateTask(dto);
        return Ok();
    }
    
    
}