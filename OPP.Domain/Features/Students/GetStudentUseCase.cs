using Microsoft.EntityFrameworkCore;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Features.Students;

public class GetStudentUseCase
{
    private readonly GantaDbContext _dbContext;

    public GetStudentUseCase(GantaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StudentDto> Execute(Guid id)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
        if(student == null)
            throw new NotFoundException(nameof(Student), id);
        
        return new StudentDto(student.Id, student.FirstName, student.LastName,
            student.Email, student.Projects.Select(p=>p.Id).ToList(),
            student.ProjectTasks.Select(x => x.Id).ToList());
    }
}