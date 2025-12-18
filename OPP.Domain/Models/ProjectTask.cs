namespace OPP.Domain;

public class ProjectTask
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Result { get; set; }
    public DateTime Deadline { get; set; }
    
    public Project Project { get; set; }
    public Guid ProjectId { get; set; }
    
    public List<ProjectTask>? Prerequisites { get; set; } = new();

    public List<ProjectTask>? DependentTasks { get; set; } = new();
    
    public Guid ResponsibleStudentId { get; set; }
}