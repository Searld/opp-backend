using Microsoft.EntityFrameworkCore;
using OPP.Domain.Data;

namespace OPP.Domain.Features.Students;

public class GetAllStudentsUseCase
{
    private readonly GantaDbContext _context;

    public GetAllStudentsUseCase(GantaDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentDto>> ExecuteAsync()
    {
        var students = await _context.Students.ToListAsync();
        return students.Select(s => new StudentDto(
            s.Id, 
            s.FirstName, 
            s.LastName, 
            s.Email,
            s.Projects.Select(p => p.Id).ToList(), 
            s.ProjectTasks.Select(t => t.Id)
                .ToList())).ToList();
    }
}