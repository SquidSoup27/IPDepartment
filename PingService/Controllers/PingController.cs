using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace PingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetPing")]
        public IActionResult Get(string query)
        {
            using var ping = new Ping();
            try
            {
                PingReply response = ping.Send(query);
                if (response != null && response.Status == IPStatus.Success)
                {
                    return Ok(
                        new PingResponse()
                        {
                            Address = response.Address.ToString(),
                            Status = response.Status.ToString(),
                            RoundtripTime = response.RoundtripTime
                        }
                    );
                }
                else
                {
                    return StatusCode(500, $"Could not ping {query}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}