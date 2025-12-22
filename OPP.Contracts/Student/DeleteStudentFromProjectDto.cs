namespace OPP.Contracts.Student;

public record DeleteStudentFromProjectDto(
    Guid ProjectId,
    Guid StudentId);