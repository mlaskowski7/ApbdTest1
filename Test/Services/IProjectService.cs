namespace Test.Services;

public interface IProjectService
{
    Task<bool> DeleteProjectByIdAsync(int id, CancellationToken cancellationToken = default);
}