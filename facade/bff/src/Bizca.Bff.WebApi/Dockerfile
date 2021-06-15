#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["facade/bff/src/Bizca.Bff.WebApi/Bizca.Bff.WebApi.csproj", "facade/bff/src/Bizca.Bff.WebApi/"]
RUN dotnet restore "facade/bff/src/Bizca.Bff.WebApi/Bizca.Bff.WebApi.csproj"
COPY . .
WORKDIR "/src/facade/bff/src/Bizca.Bff.WebApi"
RUN dotnet build "Bizca.Bff.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bizca.Bff.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bizca.Bff.WebApi.dll"]