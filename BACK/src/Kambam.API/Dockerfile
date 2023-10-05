FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Kambam.API/Kambam.API.csproj", "src/Kambam.API/"]
COPY ["src/Kambam.Infra/Kambam.Infra.csproj", "src/Kambam.Infra/"]
COPY ["src/Kambam.Domain/Kambam.Domain.csproj", "src/Kambam.Domain/"]
COPY ["src/Kambam.Service/Kambam.Service.csproj", "src/Kambam.Service/"]
RUN dotnet restore "src/Kambam.API/Kambam.API.csproj"
COPY . .
WORKDIR "/src/src/Kambam.API"
RUN dotnet build "Kambam.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kambam.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kambam.API.dll"]