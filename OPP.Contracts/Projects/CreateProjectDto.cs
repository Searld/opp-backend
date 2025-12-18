namespace OPP.Contracts.Projects;

public record CreateProjectDto(
    string Name,
    string Description,
    DateTime Deadline,
    Guid CreatorId,
    string SubjectName);