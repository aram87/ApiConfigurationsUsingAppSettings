using ApiConfigurationsUsingAppSettings.Interfaces;
using ApiConfigurationsUsingAppSettings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace ApiConfigurationsUsingAppSettings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashingController : ControllerBase
    {
        private readonly IHashingService _hashingService;
        private readonly IOptions<ApiSettingsModel> _options;

        public HashingController(IHashingService hashingService, IOptions<ApiSettingsModel> options)
        {
            _hashingService = hashingService;
            _options = options;
        }
        [HttpGet("Test")]
        public IActionResult TestEndpoint()
        {
            if (_options.Value.TestingEndpointEnabled.HasValue && _options.Value.TestingEndpointEnabled.Value)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("PbKdf2")]
        public IActionResult HashUsingPbKdf2([FromBody] string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return BadRequest("Password must be provided");
            }
            var passwordBytes = Encoding.ASCII.GetBytes(password);

            var hash = _hashingService.HashUsingPbkdf2(passwordBytes);
            
            if (string.IsNullOrEmpty(hash))
            {
                return BadRequest($"Unable to calcuate hash for {password}");
            }
        
            return Ok(hash);
        }

        [HttpPost("HMAC256")]
        public IActionResult ComputeHmac([FromBody] string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return BadRequest("Data must be provided");
            }
            var dataBytes = Encoding.ASCII.GetBytes(data);
            var hash = _hashingService.ComputeHmacUsingSha256(dataBytes);

            if (string.IsNullOrEmpty(hash))
            {
                return BadRequest($"Unable to compute HMAC-SHA256 for {data}");
            }

            return Ok(hash);
        }

    }
}