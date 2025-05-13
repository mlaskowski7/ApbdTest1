namespace Test.Contracts.Response;

public record TeamMemberResponseDto(
    string FirstName,
    string LastName,
    string Email,
    List<TaskResponseDto> AssignedTasks,
    List<TaskResponseDto> CreatedTasks);