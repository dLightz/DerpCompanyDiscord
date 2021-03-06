#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SYNTAXMain.Bots/SYNTAXMain.Bots.csproj", "SYNTAXMain.Bots/"]
COPY ["SYNTAXMain.Bots/NuGet.Config", "SYNTAXMain.Bots/"]
COPY ["SYNTAXdb.DAL.Migrations/SYNTAXdb.DAL.Migrations.csproj", "SYNTAXdb.DAL.Migrations/"]
COPY ["SYNTAXdb.DAL/SYNTAXdb.DAL.csproj", "SYNTAXdb.DAL/"]
COPY ["SYNTAXMain.Core/SYNTAXMain.Core.csproj", "SYNTAXMain.Core/"]
RUN dotnet restore "SYNTAXMain.Bots/SYNTAXMain.Bots.csproj"
COPY . .
WORKDIR "/src/SYNTAXMain.Bots"
RUN dotnet build "SYNTAXMain.Bots.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SYNTAXMain.Bots.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SYNTAXMain.Bots.dll"]
