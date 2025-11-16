FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY PravilenProjekt/PravilenProjekt.csproj PravilenProjekt/
RUN dotnet restore "PravilenProjekt/PravilenProjekt.csproj"

# Copy everything
COPY PravilenProjekt/ PravilenProjekt/

# Build and publish
WORKDIR /src/PravilenProjekt
RUN dotnet publish "PravilenProjekt.csproj" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "PravilenProjekt.dll"]