# Use official .NET 5 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Copy published output from local build
COPY ./bin/Release/net5.0/publish/ .

# Run the app
ENTRYPOINT ["dotnet", "API.dll"]
