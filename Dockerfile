# -------- Build Stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
 
# Copy only API project file first
COPY MyApp.Api/MyApp.Api.csproj MyApp.Api/
RUN dotnet restore MyApp.Api/MyApp.Api.csproj
 
# Copy full source
COPY . .
 
WORKDIR /src/MyApp.Api
RUN dotnet publish MyApp.Api.csproj -c Release -o /app/publish
 
# -------- Runtime Stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
 
ENV ASPNETCORE_URLS=http://+:80
 
COPY --from=build /app/publish .
 
EXPOSE 80
ENTRYPOINT ["dotnet", "MyApp.Api.dll"]
