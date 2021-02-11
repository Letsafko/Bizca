#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["microservices/gateway/src/Bizca.Gateway/Bizca.Gateway.csproj", "microservices/gateway/src/Bizca.Gateway/"]
RUN dotnet restore "microservices/gateway/src/Bizca.Gateway/Bizca.Gateway.csproj"
COPY . .
WORKDIR "/src/microservices/gateway/src/Bizca.Gateway"
RUN dotnet build "Bizca.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bizca.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bizca.Gateway.dll"]