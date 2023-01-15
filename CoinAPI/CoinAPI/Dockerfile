#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CoinAPI.csproj", "CoinAPI/"]
RUN dotnet restore "CoinAPI/CoinAPI.csproj"
WORKDIR "/src/CoinAPI"
COPY . .
RUN dotnet build "CoinAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoinAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "CoinAPI.dll"]