using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DNSService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DNSController : ControllerBase
    {
        private readonly ILogger<DNSController> _logger;

        public DNSController(ILogger<DNSController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDNS")]
        public IActionResult Get(string query)
        {
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(query);
                return Ok(
                    new DNSResponse()
                    {
                        Addresses = hostInfo.AddressList.Select(x => x.ToString()).ToArray(),
                        Aliases = hostInfo.Aliases
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}