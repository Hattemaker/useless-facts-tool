namespace UselessFactsTool.Services;

public interface IOptimizelyService
{
    string GetFeatureVariation(string featureKey, string userId = "user_default");
    bool IsFeatureEnabled(string featureKey, string userId = "user_default");
}

public class OptimizelyService : IOptimizelyService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<OptimizelyService> _logger;

    public OptimizelyService(IConfiguration configuration, ILogger<OptimizelyService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string GetFeatureVariation(string featureKey, string userId = "user_default")
    {
        var isEnabled = _configuration.GetValue<bool>("Optimizely:Enabled");
        
        if (!isEnabled)
        {
            _logger.LogInformation("Optimizely is disabled. Using default variation.");
            return "default";
        }

        // In a real implementation, this would use the Optimizely SDK
        // For now, returning a simulated variation
        var variations = new[] { "control", "treatment", "default" };
        var hashCode = Math.Abs(userId.GetHashCode()) % variations.Length;
        
        _logger.LogInformation($"Feature: {featureKey}, User: {userId}, Variation: {variations[hashCode]}");
        return variations[hashCode];
    }

    public bool IsFeatureEnabled(string featureKey, string userId = "user_default")
    {
        var isEnabled = _configuration.GetValue<bool>("Optimizely:Enabled");
        
        if (!isEnabled)
        {
            _logger.LogInformation("Optimizely is disabled.");
            return true; // Default to enabled for fallback behavior
        }

        // Simulate feature flag evaluation
        var variation = GetFeatureVariation(featureKey, userId);
        return variation != "control";
    }
}
