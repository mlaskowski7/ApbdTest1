using Microsoft.AspNetCore.Mvc;
using Test.Contracts.Response;
using Test.Services;

namespace Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;
    
    private readonly IProjectService _projectService;

    public TasksController(ITeamMemberService teamMemberService, IProjectService projectService)
    {
        _teamMemberService = teamMemberService;
        _projectService = projectService;
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProjectByIdAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
    {
        var isFound = await _projectService.DeleteProjectByIdAsync(id, cancellationToken);
        if (!isFound)
        {
            return NotFound($"Task with id {id} does not exist");
        }
        
        return NoContent();
    }
}