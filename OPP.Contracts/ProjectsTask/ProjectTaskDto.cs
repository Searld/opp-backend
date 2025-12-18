namespace OPP.Contracts.ProjectsTask;

public record ProjectTaskDto(
    Guid Id,
    string Name,
    string Result,
    DateTimeOffset Deadline,
    bool IsCompleted,
    Guid ProjectId,
    Guid ResponsibleStudentId,
    List<Guid> Prerequisites,
    Guid? DependentTaskId);