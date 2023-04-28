using Microsoft.AspNetCore.Mvc;
using PmSim.Shared.Contracts.Actions;
using PmSim.Shared.GameEngine;

namespace PmSim.Backend.Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private static readonly HttpClient Client = new();

    private static readonly List<Game> Games = new();
    
    [HttpPost]
    public async Task <IActionResult> CreateNewGameAsync([FromBody] GameCreationRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> ConnectToGameAsync([FromBody] ActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> SetBackgroundAsync([FromBody] SetBackgroundRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> ConductInterviewAsync([FromBody] ActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> ProcessInterviewAsync([FromBody] InterviewActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> UseOpportunityAsync([FromBody] OpportunityActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> AssignToDevelopAsync([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> AssignToInventProjectAsync([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> AssignToMakeBackupAsync([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> CancelTaskAsync([FromBody] EmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> PutProjectUpForAuctionAsync([FromBody] ProposeProjectActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> PutEmployeeUpForAuctionAsync([FromBody] ProposeEmployeeActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> PutOpportunityUpForAuctionAsync([FromBody] ProposeOpportunityActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> ParticipateInAuctionAsync([FromBody] AuctionActionRequest request)
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task <IActionResult> MakeIncidentDecisionAsync([FromBody] IncidentActionRequest request)
    {
        return Ok();
    }
}