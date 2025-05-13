using Test.Entities;
using Task = System.Threading.Tasks.Task;

namespace Test.Repositories;

public interface IProjectRepository
{
    Task DeleteProjectByIdAsync(Project project, CancellationToken cancellationToken = default);
    
    Task<Project?> FindProjectByIdAsync(int projectId, CancellationToken cancellationToken = default);
}