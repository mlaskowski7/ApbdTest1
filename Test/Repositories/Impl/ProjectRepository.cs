using Microsoft.Data.SqlClient;
using Test.Entities;
using Task = Test.Entities.Task;

namespace Test.Repositories.Impl;

public class ProjectRepository : IProjectRepository
{
    private readonly string _connectionString;

    public ProjectRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                            ?? throw new ArgumentException("Default connections string must be set");
    }

    public async System.Threading.Tasks.Task DeleteProjectByIdAsync(Project project, CancellationToken cancellationToken = default)
    {
        const string deleteTaskSql = "DELETE FROM Task WHERE IdTask = @taskId";
        const string deleteProjectSql = "DELETE FROM Project WHERE IdProject = @projectId";
        
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            // delete all tasks
            foreach (var task in project.Tasks)
            {
                await using var deleteTaskCmd = new SqlCommand(deleteTaskSql, connection, (SqlTransaction)transaction);
                deleteTaskCmd.Parameters.AddWithValue("@taskId", task.Id);
                await deleteTaskCmd.ExecuteNonQueryAsync(cancellationToken);
            }
            
            await using var deleteProjectCmd = new SqlCommand(deleteProjectSql, connection, (SqlTransaction)transaction);
            deleteProjectCmd.Parameters.AddWithValue("@projectId", project.Id);
            await deleteProjectCmd.ExecuteNonQueryAsync(cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<Project?> FindProjectByIdAsync(int projectId, CancellationToken cancellationToken = default)
    {
        const string sql = """
                           SELECT 
                               p.IdProject, 
                               p.Name, 
                               p.Deadline,
                               t.IdTask,
                               t.Name as task_name,
                               t.Description,
                               t.Deadline as task_deadline
                           FROM Project p
                           INNER JOIN Task t ON t.IdProject = p.IdProject
                           WHERE p.IdProject = @projectId
                           """;

        var projects = new Dictionary<int, Project>();

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync(cancellationToken);
        
        command.Parameters.AddWithValue("@projectId", projectId);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            if (!projects.TryGetValue(projectId, out var project))
            {
                project = new Project()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("IdProject")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Deadline = reader.GetDateTime(reader.GetOrdinal("Deadline")),
                };
                projects.Add(projectId, project);
            }

            if (reader.IsDBNull(reader.GetOrdinal("IdTask"))) continue;
            var task = new Task()
            {
                Id = reader.GetInt32(reader.GetOrdinal("IdTask")),
                Name = reader.GetString(reader.GetOrdinal("task_name")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Deadline = reader.GetDateTime(reader.GetOrdinal("task_deadline")),
            };
            project.Tasks.Add(task);
        }
        
        return projects.Values.FirstOrDefault();
    }
}