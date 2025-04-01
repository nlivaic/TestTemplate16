#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TestTemplate16.Api/TestTemplate16.Api.csproj", "src/TestTemplate16.Api/"]
COPY ["src/TestTemplate16.Data/TestTemplate16.Data.csproj", "src/TestTemplate16.Data/"]
COPY ["src/TestTemplate16.Common/TestTemplate16.Common.csproj", "src/TestTemplate16.Common/"]
COPY ["src/TestTemplate16.Core/TestTemplate16.Core.csproj", "src/TestTemplate16.Core/"]
RUN dotnet restore "src/TestTemplate16.Api/TestTemplate16.Api.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate16.Api"
RUN dotnet build "TestTemplate16.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate16.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTemplate16.Api.dll"]