namespace API
{
    public class ServiceResponse
    {
        public string ServiceName { get; set; }

        public bool Success { get; set; }

        public object? Data { get; set; }

        public string Error { get; set; }

        public ServiceResponse(string serviceName)
        {
            ServiceName = serviceName;
            Error = "";
        }
    }
}
