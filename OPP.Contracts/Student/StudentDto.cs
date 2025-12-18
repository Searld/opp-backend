namespace OPP.Domain.Features.Students;

public record StudentDto(
    Guid Id, string FirstName, 
    string LastName, string Email, Guid? ProjectId,
    List<Guid> ProjectTaskIds);