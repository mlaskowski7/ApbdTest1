namespace Test.Entities;

public class TeamMember : BaseEntity
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }

    public List<Task> AssignedTasks { get; set; } = new List<Task>();
    
    public List<Task> CreatedTasks { get; set; } = new List<Task>();
}