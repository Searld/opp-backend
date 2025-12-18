namespace OPP.Domain.Features.Students;

public record StudentDto(
    Guid Id, string FirstName, 
    string LastName, string Email, 
    List<Guid> ProjectIds,
    List<Guid> ProjectTaskIds);