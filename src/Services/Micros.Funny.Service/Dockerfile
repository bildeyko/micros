#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ANIMAL_TYPE "cat"

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["Services/Micros.Funny.Service/Micros.Funny.Service.csproj", "Services/Micros.Funny.Service/"]
RUN dotnet restore "Services/Micros.Funny.Service/Micros.Funny.Service.csproj"
COPY . .
WORKDIR "/src/Services/Micros.Funny.Service"
RUN dotnet build "Micros.Funny.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Micros.Funny.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Micros.Funny.Service.dll"]