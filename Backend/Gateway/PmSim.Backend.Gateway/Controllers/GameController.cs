using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Assets.Api.Client;
using OneGate.Backend.Core.Assets.Api.Contracts.Asset;
using OneGate.Backend.Gateway.Shared.Api;
using OneGate.Backend.Gateway.User.Api.Contracts.Asset;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.User.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "assets")]
    public class GameController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GameController> _logger;
        private readonly IAssetsApiClient _assetsApiClient;

        public GameController(ILogger<GameController> logger, IMapper mapper, IAssetsApiClient assetsApiClient)
        {
            _logger = logger;
            _mapper = mapper;
            _assetsApiClient = assetsApiClient;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssetModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get assets by specified filter")]
        public async Task<IActionResult> GetAssetsRangeAsync([FromQuery] FilterAssetsRequest request)
        {
            var filter = _mapper.Map<FilterAssetsRequest, FilterAssetsDto>(request);
            var payload = await _assetsApiClient.GetAssetsAsync(filter);

            var assets = _mapper.Map<IEnumerable<AssetDto>, IEnumerable<AssetModel>>(payload);
            return Ok(assets);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AssetModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Asset details")]
        [Route("{id}")]
        public async Task<IActionResult> GetAssetAsync([FromRoute] int id)
        {
            var payload = await _assetsApiClient.GetAssetsAsync(new FilterAssetsDto
            {
                Id = id
            });
            var assetDto = payload.FirstOrDefault();

            var assets = _mapper.Map<AssetDto, AssetModel>(assetDto);
            return StrictOk(assets);
        }
    }
}