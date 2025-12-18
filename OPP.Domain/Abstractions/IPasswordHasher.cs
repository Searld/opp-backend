namespace OPP.Domain.Services;

public interface IPasswordHasher
{
    public string Generate(string password);
    public bool Verify(string hash, string password);
}