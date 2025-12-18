using Microsoft.EntityFrameworkCore;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Features.Students;

public class GetStudentByEmail
{
    private readonly GantaDbContext _context;

    public GetStudentByEmail(GantaDbContext context)
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
            student.ProjectId,
            student.ProjectTasks.Select(pt=> pt.Id).ToList());
    }
}