namespace OPP.Contracts.ProjectsTask;

public record CreateProjectTaskDto(
    string Name,
    string Result,
    DateTimeOffset Deadline,
    Guid ProjectId,
    Guid ResponsibleStudentId,
    Guid? DependentTaskId);
    