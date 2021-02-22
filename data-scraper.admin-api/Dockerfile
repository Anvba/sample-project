FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001
COPY bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "data-scraper.admin-api.dll"]
