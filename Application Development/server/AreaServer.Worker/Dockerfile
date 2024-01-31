FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /worker

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./AreaServer.Infrastructure/ ./AreaServer.Infrastructure/
COPY ./AreaServer.Core/ ./AreaServer.Core/
COPY ./AreaServer.Worker/ ./AreaServer.Worker/
RUN dotnet restore "AreaServer.Worker/AreaServer.Worker.csproj"
WORKDIR "/src/AreaServer.Worker/"
RUN dotnet build "AreaServer.Worker.csproj" -c Release -o /worker/build

FROM build AS publish
RUN dotnet publish "AreaServer.Worker.csproj" -c Release -o /worker/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /worker
COPY --from=publish /worker/publish .
ENTRYPOINT ["dotnet", "AreaServer.Worker.dll"]
