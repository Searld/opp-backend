FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# копируем solution и csproj (важно для кеша)
COPY OPP.sln ./
COPY OPP/OPP.csproj OPP/
COPY OPP.Domain/OPP.Domain.csproj OPP.Domain/
COPY OPP.Contracts/OPP.Contracts.csproj OPP.Contracts/

RUN dotnet restore

# копируем остальной код
COPY . .

# публикуем ТОЛЬКО API проект
RUN dotnet publish OPP/OPP.csproj -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "OPP.dll"]