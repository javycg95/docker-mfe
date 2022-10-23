#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DockerMfe.ApiGateway.csproj", "ApiGateway/"]
RUN dotnet restore "ApiGateway/DockerMfe.ApiGateway.csproj"

WORKDIR "/src/ApiGateway"
COPY . .

RUN dotnet build "DockerMfe.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerMfe.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerMfe.ApiGateway.dll"]