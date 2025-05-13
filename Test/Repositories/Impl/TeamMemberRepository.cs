using Microsoft.Data.SqlClient;
using Test.Entities;

namespace Test.Repositories.Impl;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly string _connectionString;
    
    private readonly ITaskRepository _taskRepository;

    public TeamMemberRepository(IConfiguration configuration, ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentException("Default connections string must be set");
    }

    public async Task<TeamMember?> FindTeamMemberByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string selectTeamMemberSql = """
                                           SELECT IdTeamMember, FirstName, LastName, Email
                                           FROM TeamMember
                                           WHERE IdTeamMember = @id
                                           """;
        
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(selectTeamMemberSql, connection);
        await connection.OpenAsync(cancellationToken);
        
        command.Parameters.AddWithValue("@id", id);
        
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        
        var assignedTasks = await _taskRepository.GetAllTasksAssignedToMemberAsync(id, cancellationToken);
        var createdTasks = await _taskRepository.GetAllTasksCreatedByMemberAsync(id, cancellationToken);
        
        var teamMember = new TeamMember()
        {
            Id = reader.GetInt32(reader.GetOrdinal("IdTeamMember")),
            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
            LastName = reader.GetString(reader.GetOrdinal("LastName")),
            Email = reader.GetString(reader.GetOrdinal("Email")),
            AssignedTasks = assignedTasks,
            CreatedTasks = createdTasks,
        };
        
        return teamMember;
    }
}