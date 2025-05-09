FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bomberman-frontend/Bomberman-frontend.csproj", "Bomberman-frontend/"]
RUN dotnet restore "Bomberman-frontend/Bomberman-frontend.csproj"
COPY . .
WORKDIR "/src/Bomberman-frontend"
RUN dotnet build "./Bomberman-frontend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Bomberman-frontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bomberman-frontend.dll"]
