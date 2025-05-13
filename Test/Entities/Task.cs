namespace Test.Entities;

public class Task  : BaseEntity
{
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required DateTime Deadline { get; set; }
    
    public Project Project { get; set; }
    
    public TaskType TaskType { get; set; }
    
    public TeamMember AssignedTo { get; set; }
    
    public TeamMember Creator { get; set; }
    
}