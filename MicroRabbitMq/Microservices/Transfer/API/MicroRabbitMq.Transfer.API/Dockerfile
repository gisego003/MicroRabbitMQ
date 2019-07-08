FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Microservices/Transfer/API/MicroRabbitMq.Transfer.API/MicroRabbitMq.Transfer.API.csproj", "Microservices/Transfer/API/MicroRabbitMq.Transfer.API/"]
RUN dotnet restore "Microservices/Transfer/API/MicroRabbitMq.Transfer.API/MicroRabbitMq.Transfer.API.csproj"
COPY . .
WORKDIR "/src/Microservices/Transfer/API/MicroRabbitMq.Transfer.API"
RUN dotnet build "MicroRabbitMq.Transfer.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicroRabbitMq.Transfer.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicroRabbitMq.Transfer.API.dll"]