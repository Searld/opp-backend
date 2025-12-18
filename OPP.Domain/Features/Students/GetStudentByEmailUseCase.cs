using Microsoft.EntityFrameworkCore;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Features.Students;

public class GetStudentByEmailUseCase
{
    private readonly GantaDbContext _context;

    public GetStudentByEmailUseCase(GantaDbContext context)
    {
        _context = context;
    }

    public async Task<StudentDto> Execute(string email)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s=> s.Email == email);
        if(student == null)
            throw new NotFoundException(nameof(Student), email);
        
        return new StudentDto(
            student.Id,
            student.FirstName,
            student.LastName,
            student.Email,
            student.Projects.Select(p => p.Id).ToList(),
            student.ProjectTasks.Select(pt=> pt.Id).ToList());
    }
}