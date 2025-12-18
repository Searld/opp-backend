using Microsoft.EntityFrameworkCore;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Features.Students;

public class AcceptInviteInProjectUseCase
{
    private readonly GantaDbContext _context;

    public AcceptInviteInProjectUseCase(GantaDbContext context)
    {
        _context = context;
    }

    public async Task Execute(Guid studentId, Guid projectId)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s=> s.Id == studentId);
        if (student == null)
            throw new NotFoundException(nameof(Student), studentId);
        
        var project = await _context.Projects
            .Include(p=>p.Members).FirstOrDefaultAsync(p => p.Id == projectId);
        if(project == null)
            throw new NotFoundException(nameof(Project), projectId);
        project.Members.Add(student);
        
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        
    }
}