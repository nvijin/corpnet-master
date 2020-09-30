#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Corpnet.API/Corpnet.API.csproj", "Corpnet.API/"]
COPY ["Corpnet.Services/Corpnet.Services.csproj", "Corpnet.Services/"]
COPY ["Corpnet.Entities/Corpnet.Entities.csproj", "Corpnet.Entities/"]
COPY ["Corpnet.Data/Corpnet.Data.csproj", "Corpnet.Data/"]
COPY ["Corpnet.Common/Corpnet.Common.csproj", "Corpnet.Common/"]
RUN dotnet restore "Corpnet.API/Corpnet.API.csproj"
COPY . .
WORKDIR "/src/Corpnet.API"
RUN dotnet build "Corpnet.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Corpnet.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Corpnet.API.dll"]