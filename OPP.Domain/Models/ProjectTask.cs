namespace OPP.Domain;

public class ProjectTask
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
    public DateTimeOffset Deadline { get; set; }
    public bool IsCompleted { get; set; }
    
    public Project Project { get; set; }
    public Guid ProjectId { get; set; }
    
    public List<ProjectTask> Prerequisites { get; set; } = new();

    public Guid? DependentTaskId { get; set; }
    public ProjectTask? DependentTask { get; set; }
    
    public Guid ResponsibleStudentId { get; set; }
}