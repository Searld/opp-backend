namespace OPP.Contracts.ProjectsTask;

public record EditProjectTaskDto(
    string Name,
    string Result,
    DateTime Deadline,
    Guid ResponsibleStudentId,
    Guid Id);