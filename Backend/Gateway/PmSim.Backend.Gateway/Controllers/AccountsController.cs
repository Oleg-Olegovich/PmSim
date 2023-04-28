using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PmSim.Shared.Contracts.Credentials;

namespace PmSim.Backend.Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private static readonly HttpClient Client = new HttpClient();
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<TokenResponse?> SignUpAsync([FromBody] SignUpRequest request)
    {
        var response = await Client.PostAsJsonAsync(
            "api/accounts/sign_up", request);
        // Здесь нужно через DatabaseManager добавить запись о пользователе и сгенерировать TokenResponse.
        // Если какая-то ошибка, то значение токена - null.
        return new TokenResponse();
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<TokenResponse?> SignInAsync([FromBody] SignInRequest request)
    {
        // Здесь нужно найти в DatabaseManager нужную запись и сгенерировать TokenResponse.
        // Если какая-то ошибка, то возвращаем null.
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMoneyBalanceAsync([FromBody] PaymentRequest request)
    {
        // Здесь нужно найти по request.UserId нужную запись о пользователе, прибавить значение Payment.
        // Позже ещё будет добавлена проверка токена и факта оплаты (удалить этот комментарий).
        return Ok();
    }
    
    [HttpGet]
    public async Task<int> GetCurrentMoneyBalanceAsync([FromBody] BasicRequest request)
    {
        // Здесь нужно найти по request.UserId нужную запись о пользователе, вернуть значение баланса.
        return 0;
    }
}