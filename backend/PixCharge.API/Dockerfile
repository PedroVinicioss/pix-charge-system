FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

WORKDIR /src/backend/PixCharge.API
RUN dotnet restore

FROM build AS final
WORKDIR /src/backend/PixCharge.API
ENTRYPOINT ["dotnet", "watch", "run", "--urls=http://0.0.0.0:8080"]
