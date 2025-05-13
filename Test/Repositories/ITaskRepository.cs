using Task = Test.Entities.Task;

namespace Test.Repositories;

public interface ITaskRepository
{
    Task<List<Task>> GetAllTasksAssignedToMemberAsync(int memberId, CancellationToken cancellationToken = default);
    
    Task<List<Task>> GetAllTasksCreatedByMemberAsync(int memberId, CancellationToken cancellationToken = default);
}