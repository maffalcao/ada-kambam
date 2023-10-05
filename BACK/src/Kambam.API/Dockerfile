#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BACK/src/Kambam.API/Kambam.API.csproj", "BACK/src/Kambam.API/"]
COPY ["BACK/src/Kambam.Infra/Kambam.Infra.csproj", "BACK/src/Kambam.Infra/"]
COPY ["BACK/src/Kambam.Domain/Kambam.Domain.csproj", "BACK/src/Kambam.Domain/"]
COPY ["BACK/src/Kambam.Service/Kambam.Service.csproj", "BACK/src/Kambam.Service/"]
RUN dotnet restore "BACK/src/Kambam.API/Kambam.API.csproj"
COPY . .
WORKDIR "/src/BACK/src/Kambam.API"
RUN dotnet build "Kambam.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kambam.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kambam.API.dll"]