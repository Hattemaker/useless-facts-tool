# Useless Facts Tool

A hilarious ASP.NET Core web application that serves random useless facts from the [Useless Facts API](https://uselessfacts.jsph.pl/) with Optimizely Opal feature flag integration.

## Features

- 🎲 Fetches random useless facts from an external API
- 🚀 ASP.NET Core 8.0 web application
- 🎯 Optimizely Opal feature flag integration for A/B testing
- 🔧 RESTful API endpoints
- 📊 Tracks feature variations for analytics
- 🐳 Ready for deployment on Render.com

## Project Structure

```
UselessFactsTool/
├── Controllers/
│   └── FactsController.cs       # API endpoints for facts
├── Models/
│   ├── FactResponse.cs          # API response model
│   └── ApiResponse.cs           # Generic API response wrapper
├── Services/
│   ├── FactService.cs           # Fetches facts from external API
│   └── OptimizelyService.cs     # Handles feature flags
├── wwwroot/                     # Static files
├── Program.cs                   # Application startup
├── appsettings.json             # Configuration
└── UselessFactsTool.csproj      # Project file
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Git

### Installation

1. Clone the repository:
```bash
git clone https://github.com/Hattemaker/useless-facts-tool.git
cd useless-facts-tool
```

2. Navigate to the project directory:
```bash
cd UselessFactsTool
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

The application will start at `https://localhost:5001` and `http://localhost:5000`.

## API Endpoints

### Get Random Fact (with Feature Flag)
```
GET /api/facts/random
```

Returns a random useless fact with Optimizely feature flag evaluation.

**Response:**
```json
{
  "success": true,
  "data": {
    "text": "Honey never spoils...",
    "permalink": "https://uselessfacts.jsph.pl/...",
    "source": "..."
  },
  "message": "Successfully fetched a useless fact!",
  "featureVariation": "treatment"
}
```

### Get Random Fact (Bypass Feature Flags)
```
GET /api/facts/random/bypass
```

Returns a random useless fact without feature flag checks.

## Configuration

### Optimizely Setup

1. Update `appsettings.json` with your Optimizely SDK key:
```json
{
  "Optimizely": {
    "SdkKey": "YOUR_SDK_KEY_HERE",
    "Enabled": true
  }
}
```

2. In development, Optimizely is disabled by default. Enable it in `appsettings.Development.json` if needed.

## Deployment on Render.com

1. Create a new Web Service on Render.com
2. Connect your GitHub repository
3. Set the build command:
```bash
dotnet publish -c Release -o out
```

4. Set the start command:
```bash
./out/UselessFactsTool
```

5. Add environment variables if needed:
```
ASPNETCORE_ENVIRONMENT=Production
```

## Testing

### Using curl:
```bash
# Get a random fact
curl http://localhost:5000/api/facts/random

# Get a fact bypassing feature flags
curl http://localhost:5000/api/facts/random/bypass
```

### Using Postman:
Import the API endpoints and test locally before deployment.

## Technologies

- **ASP.NET Core 8.0** - Web framework
- **C# 12** - Programming language
- **Optimizely SDK** - Feature flag management
- **HttpClient** - HTTP requests to external APIs

## License

MIT License - see LICENSE file for details

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

For issues or questions, please open an issue on GitHub.
