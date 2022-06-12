#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IncirAgaci.API/IncirAgaci.API.csproj", "IncirAgaci.API/"]
RUN dotnet restore "IncirAgaci.API/IncirAgaci.API.csproj"
COPY . .
WORKDIR "/src/IncirAgaci.API"
RUN dotnet build "IncirAgaci.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IncirAgaci.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IncirAgaci.API.dll"]