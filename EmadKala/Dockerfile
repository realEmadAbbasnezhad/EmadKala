# Copyright (C) 2023-2024 Emad Abbasnezhad (real_emad_abbasnezhad@proton.me)
# 
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# 
# http://www.apache.org/licenses/LICENSE-2.0
# 
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EmadKala/EmadKala.csproj", "EmadKala/"]
RUN dotnet restore "EmadKala/EmadKala.csproj"
COPY . .
WORKDIR "/src/EmadKala"
RUN dotnet build "EmadKala.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmadKala.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmadKala.dll"]
