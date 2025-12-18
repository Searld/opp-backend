namespace OPP.Domain;

public class Student
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public List<Project> Projects { get; set; } = new();
    public List<ProjectTask> ProjectTasks { get; set; } = new();
    public List<Project> CreatedProjects { get; set; } = new();
}