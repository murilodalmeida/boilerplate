FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

ARG PROJECT_NAME=Boilerplate

COPY ./Boilerplate.sln ./Boilerplate.sln
COPY ./src/App.Api/App.Api.csproj ./src/App.Api/App.Api.csproj
COPY ./src/Core/Core.csproj ./src/Core/Core.csproj
COPY ./src/Infra/Infra.csproj ./src/Infra/Infra.csproj
COPY ./tools/Migrations/Migrations.csproj ./tools/Migrations/Migrations.csproj
COPY ./libs/Libs.AspNetCore/Libs.AspNetCore.csproj ./libs/Libs.AspNetCore/Libs.AspNetCore.csproj
COPY ./libs/Libs.Core/Libs.Core.csproj ./libs/Libs.Core/Libs.Core.csproj
COPY ./libs/Libs.Infra/Libs.Infra.csproj ./libs/Libs.Infra/Libs.Infra.csproj

RUN dotnet restore ./Boilerplate.sln

COPY . .

RUN dotnet publish ./src/App.Api/App.Api.csproj \
    -c Release \
    -r linux-x64 \
    --self-contained false \
    -o ./artifacts \
    -p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build ./app/artifacts .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT [ "dotnet", "FwksLabs.Boilerplate.App.Api.dll" ]