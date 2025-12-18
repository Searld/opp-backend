using Microsoft.EntityFrameworkCore;
using OPP.Contracts.Subjects;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Services;

public class SubjectsService
{
    private readonly GantaDbContext _context;

    public SubjectsService(GantaDbContext context)
    {
        _context = context;
    }

    public async Task AddSubject(CreateSubjectDto dto)
    {
        var subject = new Subject()
        {
            Name = dto.Name,
        };

        await _context.Subjects.AddAsync(subject);
        await _context.SaveChangesAsync();
    }

    public async Task<SubjectDto> GetSubjectById(Guid id)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        if (subject == null)
            throw new NotFoundException(nameof(Subject), id);
        return new SubjectDto(subject.Id, subject.Name);
    }

    public async Task DeleteSubject(Guid id)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        if (subject == null)
            throw new NotFoundException(nameof(Subject), id);

        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();

    }

    public async Task UpdateSubject(SubjectDto dto)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == dto.Id);
        if (subject == null)
            throw new NotFoundException(nameof(Subject), dto.Id);


        subject.Name = dto.Name;
        _context.Subjects.Update(subject);
        await _context.SaveChangesAsync();
    }

    public async Task<List<SubjectDto>> GetAllSubjects()
    {
        var subjects = await _context.Subjects.ToListAsync();
        return subjects.Select(s => new SubjectDto(s.Id, s.Name)).ToList();
    }
}