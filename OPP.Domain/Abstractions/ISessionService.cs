using OPP.Domain;

namespace OPP.Application;

public interface ISessionService
{
    public Task<bool> IsSessionValid(Guid sessionToken);
    public Task<Session> GetSessionById(Guid sessionToken);
}