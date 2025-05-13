using Test.Contracts.Response;
using Test.Entities;
using Task = Test.Entities.Task;

namespace Test.Mappers.Impl;

public class TaskMapper : IMapper<Task, TaskResponseDto>
{
    public TaskResponseDto MapEntityToResponse(Task entity)
    {
        return new TaskResponseDto(
            entity.Name, 
            entity.Description, 
            entity.Deadline, 
            entity.Project.Name, 
            entity.TaskType.Name);
    }
}