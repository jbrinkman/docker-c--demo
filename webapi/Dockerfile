FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers

COPY *.csproj ./src/

RUN dotnet restore ./src/webapi.csproj

# Copy everything else and build

COPY . ./src

RUN dotnet publish ./src/ -c Release -o out

ENTRYPOINT ["dotnet", "./out/webapi.dll"]