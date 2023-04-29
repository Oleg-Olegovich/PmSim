using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PmSim.Shared.Contracts.Credentials;

namespace PmSim.Backend.DatabaseManager.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public TokenResponse? SignUp([FromBody] SignUpRequest request)
    {
        return new TokenResponse();
    }
    
    [HttpGet]
    [AllowAnonymous]
    public TokenResponse? SignIn([FromBody] SignInRequest request)
    {
        return null;
    }

    [HttpPost]
    public IActionResult UpdateMoneyBalance([FromBody] PaymentRequest request)
    {
        return Ok();
    }
    
    [HttpGet]
    public int GetCurrentMoneyBalance([FromBody] BasicRequest request)
    {
        return 0;
    }
}