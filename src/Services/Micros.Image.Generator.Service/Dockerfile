#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

RUN apt-get update && apt-get install -y \
    fontconfig \
    && rm -rf /var/lib/apt/lists/*
COPY ["Services/Micros.Image.Generator.Service/Assets/Fonts/Roboto", "/usr/share/fonts/truetype/roboto/"]
RUN fc-cache -f -v

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Micros.Image.Generator.Service/Micros.Image.Generator.Service.csproj", "Services/Micros.Image.Generator.Service/"]
RUN dotnet restore "Services/Micros.Image.Generator.Service/Micros.Image.Generator.Service.csproj"
COPY . .
WORKDIR "/src/Services/Micros.Image.Generator.Service"
RUN dotnet build "Micros.Image.Generator.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Micros.Image.Generator.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Micros.Image.Generator.Service.dll"]