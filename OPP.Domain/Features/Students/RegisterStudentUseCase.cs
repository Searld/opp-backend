using OPP.Domain.Data;
using OPP.Domain.Services;

namespace OPP.Domain.Features.Students;

public class RegisterStudentUseCase
{
    private readonly GantaDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterStudentUseCase(GantaDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task Execute(RegisterStudentDto dto)
    {
        var passwordHash = _passwordHasher.Generate(dto.Password);
        var user = new Student()
        {
            Email = dto.Email,
            PasswordHash = passwordHash,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        await _context.Students.AddAsync(user);
        
        await _context.SaveChangesAsync();
    }
}