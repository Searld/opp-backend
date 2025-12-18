namespace OPP.Domain;

public class Subject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public List<Project> Projects { get; set; }
}