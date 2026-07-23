# Production/CI build image for Greenfolio.API.Web — distinct from .devcontainer/Dockerfile,
# which is the interactive dev-tools image. This one is what CI publishes to the registry.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Directory.Build.props Directory.Packages.props nuget.config global.json ./
COPY src/Greenfolio.API.Web/Greenfolio.API.Web.csproj src/Greenfolio.API.Web/
COPY src/Greenfolio.API.Core/Greenfolio.API.Core.csproj src/Greenfolio.API.Core/
COPY src/Greenfolio.API.Infrastructure/Greenfolio.API.Infrastructure.csproj src/Greenfolio.API.Infrastructure/
COPY src/Greenfolio.API.UseCases/Greenfolio.API.UseCases.csproj src/Greenfolio.API.UseCases/
RUN dotnet restore src/Greenfolio.API.Web/Greenfolio.API.Web.csproj

COPY src/ src/
RUN dotnet publish src/Greenfolio.API.Web/Greenfolio.API.Web.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Greenfolio.API.Web.dll"]
