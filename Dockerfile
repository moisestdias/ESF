#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# this file should be moved to the solution dir... Only here to be added to the template;

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EpmDashboard.csproj", "."]
RUN dotnet restore "EpmDashboard.csproj"
COPY . .
#WORKDIR "/src/server"
RUN dotnet build "EpmDashboard.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EpmDashboard.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EpmDashboard.dll"]


