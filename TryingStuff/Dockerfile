FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TryingStuff/TryingStuff.csproj", "TryingStuff/"]
RUN dotnet restore "TryingStuff/TryingStuff.csproj"
COPY . .
WORKDIR "/src/TryingStuff"
RUN dotnet build "TryingStuff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TryingStuff.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TryingStuff.dll"]
