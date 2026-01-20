FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY ["Orderly.Orders/Orderly.Orders.csproj", "Orderly.Orders/"]
COPY ["Orderly.Orders.Domain/Orderly.Orders.Domain.csproj", "Orderly.Orders.Domain/"]
COPY ["Orderly.Orders.Application/Orderly.Orders.Application.csproj", "Orderly.Orders.Application/"]
COPY ["Orderly.Orders.Infrastructure/Orderly.Orders.Infrastructure.csproj", "Orderly.Orders.Infrastructure/"]


RUN dotnet restore "Orderly.Orders/Orderly.Orders.csproj"


COPY . .

WORKDIR /src/Orderly.Orders
RUN dotnet build "Orderly.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orderly.Orders.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orderly.Orders.dll"]
