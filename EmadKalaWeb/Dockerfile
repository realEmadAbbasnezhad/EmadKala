# Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
# This file is a part of EmadKala, Licenced under http://www.gnu.org/licenses/

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EmadKalaWeb/EmadKalaWeb.csproj", "EmadKalaWeb/"]
RUN dotnet restore "EmadKalaWeb/EmadKalaWeb.csproj"
COPY . .
WORKDIR "/src/EmadKalaWeb"
RUN dotnet build "EmadKalaWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EmadKalaWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmadKalaWeb.dll"]
