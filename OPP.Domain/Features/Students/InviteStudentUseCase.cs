using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using OPP.Contracts.Student;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace OPP.Domain.Features.Students;

public class InviteStudentUseCase
{
    private readonly GantaDbContext _context;

    public InviteStudentUseCase(GantaDbContext context)
    {
        _context = context;
    }

    public async Task Execute(Guid inviterId, InviteStudentDto dto)
    {
        
        var project = await _context.Projects
            .FirstOrDefaultAsync(p=>p.Id == dto.ProjectId);
        if(project == null)
            throw new NotFoundException(nameof(Project), dto.ProjectId);
        var inviter = await _context.Students
            .Include(s=>s.CreatedProjects)
            .FirstOrDefaultAsync(s=>s.Id == inviterId);
        if(inviter == null)
            throw new NotFoundException(nameof(Student), inviterId);
        
        var student = _context.Students.FirstOrDefault(s => s.Email == dto.Email);
        if(student == null)
            throw new NotFoundException(nameof(Student), dto.StudentId);
        
        if(!inviter.CreatedProjects.Any(p=>p.Id == dto.ProjectId))
            throw new PermissionException("You do not have permission to invite this project");
        
        
        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync("smtp.yandex.ru", 587, SecureSocketOptions.StartTls);
            
            await smtp.AuthenticateAsync("salokrass", "jmqmfizwwiteoere");

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = $"Привет, {student.FirstName} {student.LastName}, вас пригласили в проект {project.Name}.\n" +
                                   $"Чтобы присоединиться к нему, пожалуйста, перейдите по ссылке: " +
                                   $"http://localhost:5000/api/students/accept-invite/{dto.StudentId}/{dto.ProjectId}";
            
            var msg = new MimeMessage()
            {
                Subject = $"Приглашение в проект {project.Name}",
                Body = bodyBuilder.ToMessageBody()
            };
            
            msg.To.Add(MailboxAddress.Parse(dto.Email));
            msg.From.Add(new MailboxAddress($"От {inviter.FirstName} {inviter.LastName}", "salokrass@yandex.ru"));
            
            await smtp.SendAsync(msg);
        }
    }
    
}