FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Projekt zaliczeniowy/Projekt zaliczeniowy.csproj", "Projekt zaliczeniowy/"]
RUN dotnet restore "Projekt zaliczeniowy/Projekt zaliczeniowy.csproj"

COPY . ./
WORKDIR /src/Projekt zaliczeniowy
RUN dotnet publish "Projekt zaliczeniowy.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Projekt zaliczeniowy.dll"]
