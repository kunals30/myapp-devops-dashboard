# -------- Build Stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
 
COPY *.sln .
COPY MyApp.Api/*.csproj ./MyApp.Api/
RUN dotnet restore
 
COPY . .
WORKDIR /src/MyApp.Api
RUN dotnet publish -c Release -o /app/publish
 
# -------- Runtime Stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
 
COPY --from=build /app/publish .
 
EXPOSE 80
ENTRYPOINT ["dotnet", "MyApp.Api.dll"]
