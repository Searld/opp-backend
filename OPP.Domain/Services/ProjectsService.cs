using Microsoft.EntityFrameworkCore;
using OPP.Contracts.Projects;
using OPP.Contracts.ProjectsTask;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Services;

public class ProjectsService
{
    private readonly GantaDbContext _context;

    public ProjectsService(GantaDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(CreateProjectDto dto)
    {
        Subject subject;
        if(_context.Subjects.Any(s => s.Name == dto.SubjectName))
            subject = await _context.Subjects.FirstAsync(s => s.Name == dto.SubjectName);
        else
        {
            subject = new Subject()
            {
                Name = dto.SubjectName,
            };
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }
    
        var creator = await _context.Students
            .Include(s => s.Projects) // Важно: подгружаем Projects
            .FirstOrDefaultAsync(s => s.Id == dto.CreatorId);
    
        if (creator == null)
            throw new NotFoundException(nameof(Student), dto.CreatorId);
    
        var project = new Project()
        {
            Name = dto.Name,
            Description = dto.Description,
            Deadline = dto.Deadline.ToUniversalTime(),
            CreatorId = dto.CreatorId,
            SubjectId = subject.Id,
            Members = new List<Student>() 
        };
    
        project.Members.Add(creator);
    
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateProjectDto dto)
    {
        var project =  await _context.Projects.FirstAsync(s => s.Id == dto.Id);
        if(project == null)
            throw new NotFoundException(nameof(Project), dto.Id);
        project.Name = dto.Name ?? project.Name;
        project.Description = dto.Description ?? project.Description;;
        
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }
    
    public async Task<ProjectDto> GetProjectByIdAsync(Guid id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p=> p.Id == id);
        if(project == null)
            throw new Exception("Project not found");
        return new ProjectDto(
            project.Id,
            project.Name,
            project.Description,
            project.Deadline,
            project.ProjectTasks.Select(pt => pt.Id).ToList(),
            project.Members.Select(m => m.Id).ToList(),
            project.Subject.Name,
            project.CreatorId);
    }

    public async Task<Guid> CreateProjectTaskAsync(CreateProjectTaskDto dto)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == dto.ProjectId);
        if(project == null)
            throw new NotFoundException(nameof(Project), dto.ProjectId);

        var projectTask = new ProjectTask()
        {
            Name = dto.Name,
            Deadline = dto.Deadline.ToUniversalTime(),
            DependentTaskId = dto.DependentTaskId,
            ProjectId = dto.ProjectId,
            ResponsibleStudentId = dto.ResponsibleStudentId,
            Result = dto.Result
        };
        
        await _context.ProjectTasks.AddAsync(projectTask);
        await _context.SaveChangesAsync();
        return projectTask.Id;
    }

    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
        return _context.Projects
            .Include(p=>p.ProjectTasks)
            .Include(p=>p.Members)
            .Select(p=> new ProjectDto(
            p.Id,
            p.Name,
            p.Description,
            p.Deadline,
            p.ProjectTasks.Select(pt => pt.Id).ToList(),
            p.Members.Select(m => m.Id).ToList(),
            p.Subject.Name,
            p.CreatorId)).ToList();
    }

    public async Task DeleteProjectAsync(Guid id)
    {
        var project =  await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        if(project == null)
            throw new NotFoundException(nameof(Project), id);
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
}