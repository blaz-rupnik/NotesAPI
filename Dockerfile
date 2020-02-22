FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./NotesApp/NotesApp.csproj"
WORKDIR "/src/NotesApp"
RUN dotnet build "NotesApp.csproj" -c Release -o /app

FROM build AS publish
WORKDIR "/src/NotesApp"
RUN dotnet publish "NotesApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NotesApp.dll", "--server.urls", "http://0.0.0.0:5000"]