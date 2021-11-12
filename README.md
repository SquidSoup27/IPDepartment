# IPDepartment
An API to query various services for data about an IP address or domain name.

## Usage
### Endpoint
```
https://ipdepartment.azurewebsites.net/domaininfo?query={query}&services={services}
```

### Parameters
query - A valid IP address or domain name

services - A comma separated list of services from among the following:
- ping
- dns
- geoapi
- whois

### Example
https://ipdepartment.azurewebsites.net/domaininfo/?query=8.8.8.8&services=dns,whois

## Help
Visit https://ipdepartment.azurewebsites.net/swagger/index.html for more info.
