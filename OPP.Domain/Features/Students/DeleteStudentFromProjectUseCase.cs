using Microsoft.EntityFrameworkCore;
using OPP.Contracts.Student;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Features.Students;

public class DeleteStudentFromProjectUseCase
{
    private readonly GantaDbContext _context;

    public DeleteStudentFromProjectUseCase(GantaDbContext context)
    {
        _context = context;
    }

    public async Task Execute(DeleteStudentFromProjectDto dto, Guid deletedBy)
    {
        var project = await _context.Projects.Include(p=>p.Members)
            .FirstOrDefaultAsync(p=> p.Id == dto.ProjectId);
        if (project == null)
            throw new NotFoundException(nameof(Project), dto.ProjectId);
        if (project.CreatorId != deletedBy)
            throw new PermissionException();
        if(!project.Members.Select(x => x.Id).Contains(dto.StudentId))
            throw new NotFoundException(nameof(Student), dto.StudentId);
        var student = await _context.Students.FindAsync(dto.StudentId);
        if (student == null)
            throw new NotFoundException(nameof(Student), dto.StudentId);
        project.Members.Remove(student);
        await _context.SaveChangesAsync();
    }
}