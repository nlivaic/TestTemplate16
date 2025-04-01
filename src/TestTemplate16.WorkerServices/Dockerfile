#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/TestTemplate16.WorkerServices/TestTemplate16.WorkerServices.csproj", "src/TestTemplate16.WorkerServices/"]
COPY ["src/TestTemplate16.Core/TestTemplate16.Core.csproj", "src/TestTemplate16.Core/"]
COPY ["src/TestTemplate16.Common/TestTemplate16.Common.csproj", "src/TestTemplate16.Common/"]
RUN dotnet restore "src/TestTemplate16.WorkerServices/TestTemplate16.WorkerServices.csproj"
COPY . .
WORKDIR "/src/src/TestTemplate16.WorkerServices"
RUN dotnet build "TestTemplate16.WorkerServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTemplate16.WorkerServices.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["src/TestTemplate16.WorkerServices/entrypoint.sh", "src/entrypoint.sh"]
COPY ["src/TestTemplate16.WorkerServices/wait-for-it.sh", "src/wait-for-it.sh"]
CMD ["/bin/bash", "src/entrypoint.sh"]
