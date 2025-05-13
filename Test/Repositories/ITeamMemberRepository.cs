using Test.Entities;

namespace Test.Repositories;

public interface ITeamMemberRepository
{
    Task<TeamMember?> FindTeamMemberByIdAsync(int id, CancellationToken cancellationToken = default);
}