using Test.Contracts.Response;
using Test.Entities;
using Test.Mappers;
using Test.Repositories;

namespace Test.Services.Impl;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    
    private readonly IMapper<TeamMember, TeamMemberResponseDto> _teamMemberMapper;

    public TeamMemberService(
        ITeamMemberRepository teamMemberRepository, 
        IMapper<TeamMember, TeamMemberResponseDto> teamMemberMapper)
    {
        _teamMemberRepository = teamMemberRepository;
        _teamMemberMapper = teamMemberMapper;
    }
    
    public async Task<TeamMemberResponseDto?> GetTeamMemberByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var teamMember = await _teamMemberRepository.FindTeamMemberByIdAsync(id, cancellationToken);
        if (teamMember is null)
        {
            return null;
        }
        
        return _teamMemberMapper.MapEntityToResponse(teamMember);
    }
}