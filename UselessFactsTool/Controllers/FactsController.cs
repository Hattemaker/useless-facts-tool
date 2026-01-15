using Microsoft.AspNetCore.Mvc;
using UselessFactsTool.Models;
using UselessFactsTool.Services;

namespace UselessFactsTool.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FactsController : ControllerBase
{
    private readonly IFactService _factService;
    private readonly IOptimizelyService _optimizelyService;
    private readonly ILogger<FactsController> _logger;

    public FactsController(
        IFactService factService,
        IOptimizelyService optimizelyService,
        ILogger<FactsController> logger)
    {
        _factService = factService;
        _optimizelyService = optimizelyService;
        _logger = logger;
    }

    /// <summary>
    /// Gets a random useless fact from the API
    /// </summary>
    [HttpGet("random")]
    public async Task<IActionResult> GetRandomFact()
    {
        try
        {
            // Check if feature is enabled via Optimizely
            var userId = HttpContext.User.Identity?.Name ?? "anonymous";
            var featureEnabled = _optimizelyService.IsFeatureEnabled("facts_feature", userId);
            var variation = _optimizelyService.GetFeatureVariation("facts_feature", userId);

            if (!featureEnabled)
            {
                _logger.LogInformation("Feature disabled for user {UserId}", userId);
                return Ok(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Feature is currently disabled",
                    FeatureVariation = variation
                });
            }

            var fact = await _factService.GetRandomFactAsync();

            if (fact == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Failed to fetch a useless fact",
                        FeatureVariation = variation
                    });
            }

            return Ok(new ApiResponse<FactResponse>
            {
                Success = true,
                Data = fact,
                Message = "Successfully fetched a useless fact!",
                FeatureVariation = variation
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetRandomFact");
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ApiResponse<object>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                });
        }
    }

    /// <summary>
    /// Gets a random fact without Optimizely feature flag check
    /// </summary>
    [HttpGet("random/bypass")]
    public async Task<IActionResult> GetRandomFactBypass()
    {
        try
        {
            var fact = await _factService.GetRandomFactAsync();

            if (fact == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Failed to fetch a useless fact"
                    });
            }

            return Ok(new ApiResponse<FactResponse>
            {
                Success = true,
                Data = fact,
                Message = "Successfully fetched a useless fact!"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetRandomFactBypass");
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ApiResponse<object>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                });
        }
    }
}
