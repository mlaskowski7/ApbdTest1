namespace Test.Contracts.Response;

public record TaskResponseDto(
    string Name,
    string Description,
    DateTime Deadline,
    string ProjectName,
    string TaskType);