FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Application.API/Application.API.csproj", "Application.API/"]
COPY ["Domain.ProductionPlanning/Domain.ProductionPlanning.csproj", "Domain.ProductionPlanning/"]
COPY ["Application.ProductionPlan.Shared/Application.ProductionPlan.Shared.csproj", "Application.ProductionPlan.Shared/"]
RUN dotnet restore "Application.API/Application.API.csproj"
COPY . .
WORKDIR "/src/Application.API"
RUN dotnet build "Application.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.API.dll"]