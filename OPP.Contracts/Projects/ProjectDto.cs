using OPP.Contracts.ProjectsTask;

namespace OPP.Contracts.Projects;

public record ProjectDto(
    Guid Id,
    string Name,
    string Description,
    DateTimeOffset Deadline,
    List<Guid> Tasks,
    List<Guid> Members,
    string? SubjectName,
    Guid? CreatorId);