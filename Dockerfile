# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY UselessFactsTool/*.csproj ./UselessFactsTool/
RUN dotnet restore ./UselessFactsTool/UselessFactsTool.csproj

# Copy everything else and build
COPY UselessFactsTool/. ./UselessFactsTool/
WORKDIR /app/UselessFactsTool
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/UselessFactsTool/out .

# Expose port (Render will set PORT environment variable)
ENV ASPNETCORE_URLS=http://+:${PORT:-5000}
EXPOSE ${PORT:-5000}

ENTRYPOINT ["dotnet", "UselessFactsTool.dll"]
