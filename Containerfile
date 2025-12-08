# ============================================================
# STAGE 1 — Restore & Build
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution + project files first (best caching)
COPY FizzBuzz.sln .
COPY FizzBuzz.Api/FizzBuzz.Api.csproj FizzBuzz.Api/
COPY FizzBuzz.Engine/FizzBuzz.Engine.csproj FizzBuzz.Engine/
COPY FizzBuzz.Tests/FizzBuzz.Tests.csproj FizzBuzz.Tests/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build (release)
RUN dotnet build FizzBuzz.Api/FizzBuzz.Api.csproj -c Release -o /app/build

# ============================================================
# STAGE 2 — Publish trimmed output
# ============================================================
FROM build AS publish

RUN dotnet publish FizzBuzz.Api/FizzBuzz.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# ============================================================
# STAGE 3 — Runtime image (small & production)
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "FizzBuzz.Api.dll"]