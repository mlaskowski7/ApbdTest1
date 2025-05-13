using Test.Repositories;

namespace Test.Services.Impl;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<bool> DeleteProjectByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.FindProjectByIdAsync(id, cancellationToken);
        if (project is null)
        {
            return false;
        }

        await _projectRepository.DeleteProjectByIdAsync(project, cancellationToken);
        return true;
    }
}