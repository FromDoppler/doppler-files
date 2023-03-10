#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Doppler.Files.Api/Doppler.Files.Api.csproj", "Doppler.Files.Api/"]
RUN dotnet restore "Doppler.Files.Api/Doppler.Files.Api.csproj"
COPY . .
WORKDIR "/src/Doppler.Files.Api"
RUN dotnet build "Doppler.Files.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Doppler.Files.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ARG version=unknown
RUN echo $version > /app/wwwroot/version.txt
ENTRYPOINT ["dotnet", "Doppler.Files.Api.dll"]
