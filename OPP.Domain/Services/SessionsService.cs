using Microsoft.EntityFrameworkCore;
using OPP.Application;
using OPP.Domain.Data;
using OPP.Domain.Exceptions;

namespace OPP.Domain.Services;

public class SessionsService : ISessionService
{
    private readonly GantaDbContext _context;

    public SessionsService(GantaDbContext context)
    {
        _context = context;
    }
    public async Task<bool> IsSessionValid(Guid sessionToken)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionToken);
        var now = DateTime.UtcNow;

        if (session == null)
        {
            return false;
        }

        if (session.ExpiresAt <= now)
        {
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return false;
        }
        return true;
    }

    public async Task<Session> GetSessionById(Guid sessionToken)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Id == sessionToken);
        if(session == null)
            throw new NotFoundException(nameof(Session), sessionToken);
        return session;
    }
}