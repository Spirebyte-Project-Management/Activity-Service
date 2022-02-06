#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Activity-Service/src/Spirebyte.Services.Activities.API/Spirebyte.Services.Activities.API.csproj", "src/Activity-Service/src/Spirebyte.Services.Activities.API/"]
COPY ["src/Activity-Service/src/Spirebyte.Services.Activities.Infrastructure/Spirebyte.Services.Activities.Infrastructure.csproj", "src/Activity-Service/src/Spirebyte.Services.Activities.Infrastructure/"]
COPY ["src/Activity-Service/src/Spirebyte.Services.Activities.Application/Spirebyte.Services.Activities.Application.csproj", "src/Activity-Service/src/Spirebyte.Services.Activities.Application/"]
COPY ["src/Activity-Service/src/Spirebyte.Services.Activities.Core/Spirebyte.Services.Activities.Core.csproj", "src/Activity-Service/src/Spirebyte.Services.Activities.Core/"]
RUN dotnet restore "src/Activity-Service/src/Spirebyte.Services.Activities.API/Spirebyte.Services.Activities.API.csproj"
COPY . .
WORKDIR "/src/src/Activity-Service/src/Spirebyte.Services.Activities.API"
RUN dotnet build "Spirebyte.Services.Activities.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Spirebyte.Services.Activities.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spirebyte.Services.Activities.API.dll"]
