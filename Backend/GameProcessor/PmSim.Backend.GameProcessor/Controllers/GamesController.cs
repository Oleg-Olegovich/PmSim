using Microsoft.AspNetCore.Mvc;
using PmSim.Shared.Contracts.Actions;

namespace PmSim.Backend.GameProcessor.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    [HttpPost("create-new-game")]
    public IActionResult CreateNewGame([FromBody] GameCreationRequest request)
    {
        Console.WriteLine("Create new game!");
        return Ok();
    }

    [HttpPost("connect-to-game")]
    public IActionResult ConnectToGame([FromBody] ActionRequest request)
    {
        return Ok();
    }

    [HttpPost("set-background")]
    public IActionResult SetBackground([FromBody] SetBackgroundRequest request)
    {
        return Ok();
    }

    [HttpPost("conduct-interview")]
    public IActionResult ConductInterview([FromBody] ActionRequest request)
    {
        return Ok();
    }

    [HttpPost("process-interview")]
    public IActionResult ProcessInterview([FromBody] InterviewActionRequest request)
    {
        return Ok();
    }

    [HttpPost("use-opportunity")]
    public IActionResult UseOpportunity([FromBody] OpportunityActionRequest request)
    {
        return Ok();
    }

    [HttpPost("assign-to-develop")]
    public IActionResult AssignToDevelop([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }

    [HttpPost("assign-to-invent-project")]
    public IActionResult AssignToInventProject([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }

    [HttpPost("assign-to-make-backup")]
    public IActionResult AssignToMakeBackup([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }

    [HttpPost("cancel-task")]
    public IActionResult CancelTask([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }

    [HttpPost("put-project-up-for-auction")]
    public IActionResult PutProjectUpForAuction([FromBody] ProposeProjectActionRequest request)
    {
        return Ok();
    }

    [HttpPost("put-employee-up-for-auction")]
    public IActionResult PutEmployeeUpForAuction([FromBody] ProposeEmployeeActionRequest request)
    {
        return Ok();
    }

    [HttpPost("put-opportunity-up-for-auction")]
    public IActionResult PutOpportunityUpForAuction([FromBody] ProposeOpportunityActionRequest request)
    {
        return Ok();
    }

    [HttpPost("participate-in-auction")]
    public IActionResult ParticipateInAuction([FromBody] AuctionActionRequest request)
    {
        return Ok();
    }

    [HttpPost("make-incident-decision")]
    public IActionResult MakeIncidentDecision([FromBody] IncidentActionRequest request)
    {
        return Ok();
    }

}