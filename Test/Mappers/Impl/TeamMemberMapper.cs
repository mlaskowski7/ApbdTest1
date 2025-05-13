using Test.Contracts.Response;
using Test.Entities;
using Test.Services;
using Task = Test.Entities.Task;

namespace Test.Mappers.Impl;

public class TeamMemberMapper : IMapper<TeamMember, TeamMemberResponseDto>
{
    private readonly IMapper<Task, TaskResponseDto> _taskMapper;

    public TeamMemberMapper(IMapper<Task, TaskResponseDto> taskMapper)
    {
        _taskMapper = taskMapper;
    }
    
    public TeamMemberResponseDto MapEntityToResponse(TeamMember entity)
    {
        var assignedTasks = entity.AssignedTasks
                                                     .Select(_taskMapper.MapEntityToResponse)
                                                     .ToList();
        var createdTasks = entity.CreatedTasks.Select(_taskMapper.MapEntityToResponse).ToList();
        
        return new TeamMemberResponseDto(entity.FirstName, entity.LastName, entity.Email, assignedTasks, createdTasks);
    }
}