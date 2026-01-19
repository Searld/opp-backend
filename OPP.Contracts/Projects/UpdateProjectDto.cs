namespace OPP.Contracts.Projects;

public record UpdateProjectDto(Guid Id, string? Name, string? Description);