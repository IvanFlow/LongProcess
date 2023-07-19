#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-with-spa
RUN bash -E $(curl -fsSL https://deb.nodesource.com/setup_18.x | bash - ); apt install -y nodejs

FROM build-with-spa AS build
WORKDIR /src
COPY ["LongRuning.csproj", "LongRuning/"]
RUN dotnet restore "LongRuning/LongRuning.csproj"

WORKDIR "/src/LongRuning"
COPY . .

RUN dotnet build "LongRuning.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LongRuning.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LongRuning.dll"]