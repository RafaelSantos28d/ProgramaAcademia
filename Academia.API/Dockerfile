FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["Academia.API/Academia.API.csproj", "Academia.API/"]
COPY ["Academia.Application/Academia.Application.csproj", "Academia.Application/"]
COPY ["Academia.Domain/Academia.Domain.csproj", "Academia.Domain/"]
COPY ["Academia.InfraIoC/Academia.InfraIoC.csproj", "Academia.InfraIoC/"]
COPY ["Academia.Infrastructure/Academia.Infrastructure.csproj", "Academia.Infrastructure/"]

RUN dotnet restore "Academia.API/Academia.API.csproj"


COPY . .

WORKDIR "/src/Academia.API"

RUN dotnet build "Academia.API.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Academia.API.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 10000

ENTRYPOINT ["dotnet", "Academia.API.dll"]