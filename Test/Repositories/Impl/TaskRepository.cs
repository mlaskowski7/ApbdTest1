using Microsoft.Data.SqlClient;
using Test.Entities;
using Task = Test.Entities.Task;

namespace Test.Repositories.Impl;

public class TaskRepository : ITaskRepository
{
    private readonly string _connectionString;

    public TaskRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentException("Default connections string must be set");
    }

    public async Task<List<Task>> GetAllTasksAssignedToMemberAsync(int memberId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
                           SELECT 
                               t.IdTask, 
                               t.Name, 
                               t.Description, 
                               t.Deadline, 
                               p.IdProject, 
                               p.Name as project_name, 
                               p.Deadline as project_deadline, 
                               tt.IdTaskType,
                               tt.Name as task_type_name
                           FROM Task t
                           INNER JOIN Project p ON p.IdProject = t.IdProject
                           INNER JOIN TaskType tt ON t.IdTaskType = tt.IdTaskType
                           WHERE t.IdAssignedTo = @memberId
                           """;
        
        var tasks = new List<Task>();
        
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync(cancellationToken);
        
        command.Parameters.AddWithValue("@memberId", memberId);
        
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var project = new Project()
            {
                Id = reader.GetInt32(reader.GetOrdinal("IdProject")),
                Name = reader.GetString(reader.GetOrdinal("project_name")),
                Deadline = reader.GetDateTime(reader.GetOrdinal("project_deadline")),
            };

            var taskType = new TaskType()
            {
                Id = reader.GetInt32(reader.GetOrdinal("IdTaskType")),
                Name = reader.GetString(reader.GetOrdinal("task_type_name")),
            };

            tasks.Add(new Task()
            {
                Id = reader.GetInt32(reader.GetOrdinal("IdTask")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Deadline = reader.GetDateTime(reader.GetOrdinal("Deadline")),
                Project = project,
                TaskType = taskType,
            });
        }
        
        return tasks;
    }

    public async Task<List<Task>> GetAllTasksCreatedByMemberAsync(int memberId, CancellationToken cancellationToken = default)
    {
        const string sql = """
                           SELECT 
                               t.IdTask, 
                               t.Name, 
                               t.Description, 
                               t.Deadline, 
                               p.IdProject, 
                               p.Name as project_name, 
                               p.Deadline as project_deadline, 
                               tt.IdTaskType,
                               tt.Name as task_type_name
                           FROM Task t
                           INNER JOIN Project p ON p.IdProject = t.IdProject
                           INNER JOIN TaskType tt ON t.IdTaskType = tt.IdTaskType
                           WHERE t.IdCreator = @memberId
                           """;
        
        var tasks = new List<Task>();
        
        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync(cancellationToken);
        
        command.Parameters.AddWithValue("@memberId", memberId);
        
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var project = new Project()
            {
                Id = reader.GetInt32(reader.GetOrdinal("IdProject")),
                Name = reader.GetString(reader.GetOrdinal("project_name")),
                Deadline = reader.GetDateTime(reader.GetOrdinal("project_deadline")),
            };

            var taskType = new TaskType()
            {
                Id = reader.GetInt32(reader.GetOrdinal("IdTaskType")),
                Name = reader.GetString(reader.GetOrdinal("task_type_name")),
            };

            tasks.Add(new Task()
            {
                Id = reader.GetInt32(reader.GetOrdinal("IdTask")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Deadline = reader.GetDateTime(reader.GetOrdinal("Deadline")),
                Project = project,
                TaskType = taskType,
            });
        }
        
        return tasks;
    }
}