#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FriskInventarAPI.csproj", "."]
RUN dotnet restore "./FriskInventarAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FriskInventarAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FriskInventarAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FriskInventarAPI.dll"]
#ENTRYPOINT ["/bin/bash"]