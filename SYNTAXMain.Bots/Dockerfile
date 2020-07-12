FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
COPY ./NuGet.Config ./
RUN dotnet restore

# Copy and publish app and libraries
COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out ./

COPY config.json ./
ENTRYPOINT ["dotnet", "SYNTAXMain.Bots.dll"]