using Microsoft.AspNetCore.Mvc;

namespace UselessFactsTool.Controllers;

[ApiController]
[Route("[controller]")]
public class DiscoveryController : ControllerBase
{
    [HttpGet]
    public IActionResult GetApiInfo()
    {
        var apiInfo = new
        {
            name = "Useless Facts Tool API",
            version = "1.0.0",
            description = "A funny API that serves random useless facts with Optimizely feature flag integration",
            baseUrl = $"{Request.Scheme}://{Request.Host}",
            endpoints = new[]
            {
                new
                {
                    path = "/",
                    method = "GET",
                    description = "Welcome message"
                },
                new
                {
                    path = "/api/facts/random",
                    method = "GET",
                    description = "Get a random useless fact with feature flag evaluation"
                },
                new
                {
                    path = "/api/facts/random/bypass",
                    method = "GET",
                    description = "Get a random useless fact without feature flags"
                },
                new
                {
                    path = "/discovery",
                    method = "GET",
                    description = "API discovery endpoint (this endpoint)"
                }
            },
            externalApi = new
            {
                name = "Useless Facts API",
                url = "https://uselessfacts.jsph.pl/api/v2/facts/random"
            }
        };

        return Ok(apiInfo);
    }
}
