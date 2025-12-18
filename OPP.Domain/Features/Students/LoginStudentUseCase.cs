using Microsoft.EntityFrameworkCore;
using OPP.Domain.Data;
using OPP.Domain.Services;

namespace OPP.Domain.Features.Students;

public class LoginStudentUseCase
{
    private readonly GantaDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public LoginStudentUseCase(GantaDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Execute(LoginStudentDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Password) ||  string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new Exception("Invalid credentials");
        }
        
        var student = await _context.Students
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (student == null || !_passwordHasher.Verify(student.PasswordHash ?? "", dto.Password))
        {
            throw new Exception("Invalid credentials");
        }

        var now = DateTime.UtcNow;
        var session = new Session
        {
            Id = Guid.NewGuid(),
            UserId = student.Id,
            ExpiresAt = now.AddMinutes(10)
        };
        
        await _context.Sessions.AddAsync(session);
        await _context.SaveChangesAsync();
        return session.Id;
    }
}