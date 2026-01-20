FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY ["Orderly.Catalog/Orderly.Catalog.csproj", "Orderly.Catalog/"]
COPY ["Orderly.Catalog.Application/Orderly.Catalog.Application.csproj", "Orderly.Catalog.Application/"]
COPY ["Orderly.Catalog.Domain/Orderly.Catalog.Domain.csproj", "Orderly.Catalog.Domain/"]
COPY ["Orderly.Catalog.Infrastructure/Orderly.Catalog.Infrastructure.csproj", "Orderly.Catalog.Infrastructure/"]


RUN dotnet restore "Orderly.Catalog/Orderly.Catalog.csproj"


COPY . .

WORKDIR /src/Orderly.Catalog
RUN dotnet build "Orderly.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orderly.Catalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orderly.Catalog.dll"]
