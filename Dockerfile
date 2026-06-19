# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["global.json", "./"]
COPY ["src/Waterworks.Shared/Waterworks.Shared.csproj", "src/Waterworks.Shared/"]
COPY ["src/Waterworks.Data/Waterworks.Data.csproj", "src/Waterworks.Data/"]
COPY ["src/Waterworks.Api/Waterworks.Api.csproj", "src/Waterworks.Api/"]
COPY ["src/Waterworks.Web/Waterworks.Web.csproj", "src/Waterworks.Web/"]

RUN dotnet restore "src/Waterworks.Api/Waterworks.Api.csproj"
RUN dotnet restore "src/Waterworks.Web/Waterworks.Web.csproj"

COPY . .

RUN dotnet publish "src/Waterworks.Web/Waterworks.Web.csproj" -c Release -o /app/web /p:UseAppHost=false
RUN dotnet publish "src/Waterworks.Api/Waterworks.Api.csproj" -c Release -o /app/api /p:UseAppHost=false
RUN mkdir -p /app/api/wwwroot && cp -R /app/web/wwwroot/. /app/api/wwwroot/

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__Waterworks="Data Source=/tmp/waterworks.local.db"

EXPOSE 8080

COPY --from=build /app/api .

ENTRYPOINT ["dotnet", "Waterworks.Api.dll"]
