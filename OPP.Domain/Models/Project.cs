namespace OPP.Domain;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    
    public List<Task> Tasks { get; set; }
    
    public Guid CreatorId { get; set; }
    
    public Subject Subject { get; set; }
    public Guid SubjectId { get; set; }
}