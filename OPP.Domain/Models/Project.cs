namespace OPP.Domain;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset Deadline { get; set; }

    public List<ProjectTask> ProjectTasks { get; set; } = new();
    public List<Student> Members { get; set; } = new();
    
    public Guid CreatorId { get; set; }
    
    public Subject? Subject { get; set; }
    public Guid? SubjectId { get; set; }
}