# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
ENV ASPNETCORE_URLS=http://+80
# Copy csproj and restore as distinct layers
# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY *.sln ./
COPY JustTradeIt.Software.API/*.csproj ./JustTradeIt.Software.API/
COPY JustTradeIt.Software.API.Models/*.csproj ./JustTradeIt.Software.API.Models/
COPY JustTradeIt.Software.API.Repositories/*.csproj ./JustTradeIt.Software.API.Repositories/
COPY JustTradeIt.Software.API.Services/*.csproj ./JustTradeIt.Software.API.Services/
COPY JustTradeIt.Software.API/data.db ./JustTradeIt.Software.API/
RUN dotnet restore

# Copy everything else and build
WORKDIR /app
COPY . ./
WORKDIR /app/JustTradeIt.Software.API
RUN export PATH="$PATH:/root/.dotnet/tools" &&\
    dotnet tool install -g dotnet-ef &&\
    dotnet ef migrations add InitDb &&\
    dotnet ef database update
WORKDIR /app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "JustTradeIt.Software.API.dll"]



# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# LABEL maintainer="Arnar Olafsson"
# WORKDIR /app
# ENV ASPNETCORE_URLS=http://+80
# ENV ASPNETCORE_ENVIRONMENT=Development

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src
# COPY *.sln ./
# COPY JustTradeIt.Software.API/*.csproj ./JustTradeIt.Software.API/
# COPY JustTradeIt.Software.API.Models/*.csproj ./JustTradeIt.Software.API.Models/
# COPY JustTradeIt.Software.API.Repositories/*.csproj ./JustTradeIt.Software.API.Repositories/
# COPY JustTradeIt.Software.API.Services/*.csproj ./JustTradeIt.Software.API.Services/

# RUN dotnet restore
# COPY . .
# WORKDIR /src/JustTradeIt.Software.API
# RUN export PATH="$PATH:/root/.dotnet/tools" &&\
#     dotnet tool install -g dotnet-ef &&\
#     dotnet ef migrations add InitDb &&\
#     dotnet ef database update
# FROM build AS publish
# RUN dotnet publish -c Release -o /app

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app .
# ENTRYPOINT [ "dotnet", "JustTradeIt.Software.API.dll" ]
