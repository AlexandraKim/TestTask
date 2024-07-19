# Stage 1: Build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5162

# Copy and restore dependencies for all projects
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /
COPY ["TestTask.Application/TestTask.Application.csproj", "TestTask.Application/"]
COPY ["TestTask.Core/TestTask.Core.csproj", "TestTask.Core/"]
COPY ["TestTask.Infrastructure/TestTask.Infrastructure.csproj", "TestTask.Infrastructure/"]
COPY ["TestTask.Web/TestTask.Web.csproj", "TestTask.Web/"]
COPY ["TestTask.UnitTests/TestTask.UnitTests.csproj", "TestTask.UnitTests/"]
RUN dotnet restore "TestTask.Web/TestTask.Web.csproj"

# Copy the entire source code and build the project
COPY . .
WORKDIR "/TestTask.Web"
RUN dotnet build "TestTask.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 2: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TestTask.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTask.Web.dll"]
