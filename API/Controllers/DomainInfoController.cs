using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Nodes;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DomainInfoController : ControllerBase
    {
        private readonly ILogger<DomainInfoController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public DomainInfoController(ILogger<DomainInfoController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Gets domain info from specified services
        /// </summary>
        /// <param name="query">IP address or domain name</param>
        /// <param name="services">Comma separated list of services to query. Options: ping, dns, geoapi, whois. Example: "ping,whois"</param>
        /// <returns>List of services that were queried, along with data and an error message if the query failed</returns>
        /// <response code="200">Returns data from the queried services</response>
        /// <response code="400">If the query string is not a recognizable IP address or domain name</response>
        [HttpGet(Name = "GetDomainInfo")]
        public async Task<IActionResult> Get(string query, string? services = null)
        {
            if (!IsValidIPAddress(query) && !IsValidDomainName(query)) {
                return BadRequest($"{query} not recognized as a valid IP address or domain name.");
            }

            if (string.IsNullOrEmpty(services))
            {
                services = "ping,dns,geoapi,whois";
            }

            // Create a task for each service
            List<Task<ServiceResponse>> tasks = new();
            foreach (var serviceName in services.Split(","))
            {
                tasks.Add(GetFromService(serviceName, query));
            }
            
            // Wait for all tasks to complete
            await Task.WhenAll(tasks.ToArray());

            return Ok(tasks.ConvertAll(x => x.Result));
        }

        private bool IsValidIPAddress(string query)
        {
            return IPAddress.TryParse(query, out _);
        }

        private bool IsValidDomainName(string query)
        {
            return Uri.CheckHostName(query) != UriHostNameType.Unknown;
        }

        // Create a task to query a service
        private async Task<ServiceResponse> GetFromService(string serviceName, string query)
        {
            ServiceResponse serviceResponse = new(serviceName);
            var client = _clientFactory.CreateClient();
            try
            {
                string requestUrl = ServiceMap.GetServiceUrl(serviceName, query);
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                string content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    serviceResponse.Data = JsonValue.Parse(content);
                    serviceResponse.Success = true;
                }
                else
                {
                    serviceResponse.Error = $"{(int)response.StatusCode} ({response.StatusCode}): {content}";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Error = ex.Message;
            }
            return serviceResponse;
        }
    }
}