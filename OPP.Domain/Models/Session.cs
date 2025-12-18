namespace OPP.Domain;

public class Session
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime ExpiresAt { get; set; }
}