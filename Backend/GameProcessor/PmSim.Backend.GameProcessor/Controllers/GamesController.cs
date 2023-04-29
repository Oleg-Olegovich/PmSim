using Microsoft.AspNetCore.Mvc;
using PmSim.Shared.Contracts.Actions;

namespace PmSim.Backend.GameProcessor.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateNewGame([FromBody] GameCreationRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult ConnectToGame([FromBody] ActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult SetBackground([FromBody] SetBackgroundRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult ConductInterview([FromBody] ActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult ProcessInterview([FromBody] InterviewActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult UseOpportunity([FromBody] OpportunityActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult AssignToDevelop([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult AssignToInventProject([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult AssignToMakeBackup([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult CancelTask([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult PutProjectUpForAuction([FromBody] ProposeProjectActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult PutEmployeeUpForAuction([FromBody] ProposeEmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult PutOpportunityUpForAuction([FromBody] ProposeOpportunityActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult ParticipateInAuction([FromBody] AuctionActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult MakeIncidentDecision([FromBody] IncidentActionRequest request)
    {
        return Ok();
    }
}