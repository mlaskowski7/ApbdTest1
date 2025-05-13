namespace Test.Entities;

public class Project : BaseEntity
{
    public required string Name { get; set; }
    
    public required DateTime Deadline { get; set; }
    
    public List<Task> Tasks { get; set; } = new List<Task>();
}