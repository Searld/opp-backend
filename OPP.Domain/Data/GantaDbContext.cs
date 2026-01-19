using Microsoft.EntityFrameworkCore;

namespace OPP.Domain.Data;

public class GantaDbContext(DbContextOptions<GantaDbContext> options) : DbContext(options)
{
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ProjectTask>(builder =>
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.Project)
                .WithMany(p => p.ProjectTasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.DependentTask)
                .WithMany(t => t.Prerequisites)
                .HasForeignKey(t => t.DependentTaskId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne<Student>()                
                .WithMany(s => s.ProjectTasks)    
                .HasForeignKey(t => t.ResponsibleStudentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<Student>(builder =>
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(p => p.Projects)
                .WithMany(s => s.Members)
                .UsingEntity(j => j.ToTable("ProjectMembers"));

            builder.HasMany(s => s.ProjectTasks)
                .WithOne()                 
                .HasForeignKey("ResponsibleStudentId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.CreatedProjects)
                .WithOne()        
                .HasForeignKey("CreatorId")
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        
        
        
    }
}