using Microsoft.AspNetCore.Mvc;
using Test.Contracts.Response;
using Test.Services;

namespace Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;

    public TasksController(ITeamMemberService teamMemberService)
    {
        _teamMemberService = teamMemberService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TeamMemberResponseDto>> GetTeamMemberByIdAsync(
        [FromRoute] int id, 
        CancellationToken cancellationToken = default)
    {
        var teamMember = await _teamMemberService.GetTeamMemberByIdAsync(id, cancellationToken);
        if (teamMember is null)
        {
            return NotFound($"Team Member with id {id} does not exist");
        }
        
        return Ok(teamMember);
    }
}