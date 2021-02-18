FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
COPY bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "data-scraper.publisher.dll"]
