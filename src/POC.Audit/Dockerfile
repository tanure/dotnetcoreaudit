FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["POC.Audit/POC.Audit.csproj", "POC.Audit/"]
RUN dotnet restore "POC.Audit/POC.Audit.csproj"
COPY . .
WORKDIR "/src/POC.Audit"
RUN dotnet build "POC.Audit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POC.Audit.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POC.Audit.dll"]