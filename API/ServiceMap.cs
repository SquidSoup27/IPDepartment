namespace API
{
    public static class ServiceMap
    {
        // Get the url to query for a given service
        public static string GetServiceUrl(string serviceName, string query)
        {
            switch (serviceName)
            {
                case "ping":
                    return $"http://pingservice/ping?query={query}";
                case "dns":
                    return $"http://dnsservice/dns?query={query}";
                case "geoapi":
                    return $"http://geoapi:8080/{query}";
                case "whois":
                    return $"http://whois/api/whois/{query}";
                default:
                    throw new Exception($"Service not recognized: {serviceName}");
            }
        }
    }
}
