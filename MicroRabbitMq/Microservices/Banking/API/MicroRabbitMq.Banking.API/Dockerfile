FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Microservices/Banking/API/MicroRabbitMq.Banking.API/MicroRabbitMq.Banking.API.csproj", "Microservices/Banking/API/MicroRabbitMq.Banking.API/"]
RUN dotnet restore "Microservices/Banking/API/MicroRabbitMq.Banking.API/MicroRabbitMq.Banking.API.csproj"
COPY . .
WORKDIR "/src/Microservices/Banking/API/MicroRabbitMq.Banking.API"
RUN dotnet build "MicroRabbitMq.Banking.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MicroRabbitMq.Banking.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MicroRabbitMq.Banking.API.dll"]