# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src

COPY Hipster.Api Hipster.Api

# Ideally should run restore before build but it's not critical
RUN dotnet publish Hipster.Api -o Hipster.Api/artifacts -c Release


# Run
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as run
WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY --from=build /src/Hipster.Api/artifacts ./

ENTRYPOINT [ "dotnet", "Hipster.Api.dll"]