#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["Services/Micros.Api.Service/Micros.Api.Service.csproj", "Services/Micros.Api.Service/"]
RUN dotnet restore "Services/Micros.Api.Service/Micros.Api.Service.csproj"
COPY . .
WORKDIR "/src/Services/Micros.Api.Service"
RUN dotnet build "Micros.Api.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Micros.Api.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Micros.Api.Service.dll"]