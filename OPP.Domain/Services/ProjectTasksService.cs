using Microsoft.EntityFrameworkCore;
using OPP.Contracts.ProjectsTask;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Services;

public class ProjectTasksService
{
    private readonly GantaDbContext _context;

    public ProjectTasksService(GantaDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<ProjectTaskDto>> GetAllProjectTasks(Guid projectId)
    {
        var tasks =  await _context.ProjectTasks
            .Where(p=>p.ProjectId == projectId).ToListAsync();
        return tasks.Select(t=> new ProjectTaskDto(
            t.Id,
            t.Name,
            t.Result,
            t.Deadline,
            t.IsCompleted,
            t.ProjectId,
            t.ResponsibleStudentId,
            t.Prerequisites.Select(p=>p.Id).ToList(),
            t.DependentTaskId)).ToList();
    }

    public async Task ChangeTaskStatus(Guid taskId)
    {
         var projectTask = await _context.ProjectTasks.FirstOrDefaultAsync(pt=> pt.Id == taskId); 
         if(projectTask == null)
             throw new NotFoundException(nameof(ProjectTask), taskId);
         projectTask.IsCompleted = !projectTask.IsCompleted;
         await _context.SaveChangesAsync();
    }

    public async Task DeleteTask(Guid taskId)
    {
        var projectTask = await _context.ProjectTasks.FirstOrDefaultAsync(pt=> pt.Id == taskId);
        if(projectTask == null)
            throw new NotFoundException(nameof(ProjectTask), taskId);
        _context.ProjectTasks.Remove(projectTask);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTask(EditProjectTaskDto task)
    {
        var projectTask = await _context.ProjectTasks.FirstOrDefaultAsync(pt=> pt.Id == task.Id);
        if(projectTask == null)
            throw new NotFoundException(nameof(ProjectTask), task.Id);
        
        projectTask.Name = task.Name;
        projectTask.Result = task.Result;
        projectTask.Deadline = task.Deadline.ToUniversalTime();
        projectTask.ResponsibleStudentId = task.ResponsibleStudentId;
        
        _context.ProjectTasks.Update(projectTask);
        await _context.SaveChangesAsync();
    }
}