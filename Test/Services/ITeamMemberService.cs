using Test.Contracts.Response;

namespace Test.Services;

public interface ITeamMemberService
{
    Task<TeamMemberResponseDto?> GetTeamMemberByIdAsync(int id, CancellationToken cancellationToken = default);
}