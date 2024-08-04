FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmadKala.WebApp/EmadKala.WebApp.csproj", "EmadKala.WebApp/"]
RUN dotnet restore "EmadKala.WebApp/EmadKala.WebApp.csproj"
COPY . .
WORKDIR "/src/EmadKala.WebApp"
RUN dotnet build "EmadKala.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EmadKala.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmadKala.WebApp.dll"]
