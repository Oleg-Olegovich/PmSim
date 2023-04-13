using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PmSim.Backend.Gateway.Controllers
{
    //[ApiVersion("1")]
    //[Route(RouteBase + "accounts")]
    public class AccountsController //: BaseController
    {
        /*
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;
        private readonly IUsersApiClient _usersApiClient;
        private readonly IHashProvider _hashProvider;

        private readonly JwtOptions _authenticationOptions;

        public AccountsController(ILogger<AccountsController> logger,
            IOptions<JwtOptions> authenticationOptions, IMapper mapper,
            IUsersApiClient usersApiClient, IHashProvider hashProvider)
        {
            _logger = logger;
            _mapper = mapper;
            _usersApiClient = usersApiClient;
            _hashProvider = hashProvider;
            _authenticationOptions = authenticationOptions.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("Create new account")]
        public async Task<IActionResult> CreateAccountAsync([FromBody] CreateAccountRequest request)
        {
            if (request.ClientFingerprint != _authenticationOptions.ClientFingerprint)
                return Challenge();

            var accountDto = _mapper.Map<CreateAccountRequest, CreateAccountDto>(request);

            accountDto.Password = _hashProvider.Hash(accountDto.Password);
            await _usersApiClient.CreateAccountAsync(accountDto);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(AccountModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Current account details")]
        public async Task<IActionResult> GetCurrentAccountAsync()
        {
            var payload = await _usersApiClient.GetAccountsAsync(new FilterAccountsDto
            {
                Id = User.GetAccountId()
            });
            var accountDto = payload.FirstOrDefault();

            var account = _mapper.Map<AccountDto, AccountModel>(accountDto);
            return StrictOk(account);
        }
        //*/
    }
}