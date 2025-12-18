using Microsoft.EntityFrameworkCore;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Features.Students;

public class GetStudentByIdUseCase
{
    private readonly GantaDbContext _context;

    public GetStudentByIdUseCase(GantaDbContext context)
    {
        _context = context;
    }

    public async Task<StudentDto> Execute(Guid studentId)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s=>s.Id==studentId);
        if(student == null)
            throw new NotFoundException(nameof(Student), studentId);
        return new StudentDto(
            student.Id,
            student.FirstName,
            student.LastName,
            student.Email,
            student.Projects.Select(p => p.Id).ToList(),
            student.ProjectTasks.Select(s=>s.Id).ToList());
    }
}