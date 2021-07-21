FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-build-base
WORKDIR /src
COPY MODiX.sln .
COPY Directory.* .
COPY **/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore MODiX.sln
COPY . . 

FROM dotnet-build-base AS dotnet-build
RUN dotnet build -c Release --no-restore MODiX.sln

FROM dotnet-build as dotnet-test
RUN dotnet test -c Release --no-build --no-restore MODiX.sln

FROM dotnet-build AS publish
RUN dotnet publish -c Release --no-build --no-restore -o /app  MODiX.Host/MODiX.Host.csproj

FROM base AS final
COPY --from=publish /app .
HEALTHCHECK CMD curl --fail http://localhost:80/health || exit 1
ENTRYPOINT ["dotnet", "MODiX.Host.dll"]
