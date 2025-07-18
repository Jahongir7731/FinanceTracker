FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FinanceTracker.API/FinanceTracker.API.csproj", "FinanceTracker.API/"]
RUN dotnet restore "FinanceTracker.API/FinanceTracker.API.csproj"
COPY . .
WORKDIR "/src/FinanceTracker.API"
RUN dotnet build "FinanceTracker.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinanceTracker.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinanceTracker.API.dll"]
ENV ASPNETCORE_URLS=http://+:80
