FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PixCharge.API/PixCharge.API.csproj", "PixCharge.API/"]
RUN dotnet restore "PixCharge.API/PixCharge.API.csproj"
COPY . .
WORKDIR "/src/PixCharge.API"
RUN dotnet build "PixCharge.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PixCharge.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PixCharge.API.dll"]
