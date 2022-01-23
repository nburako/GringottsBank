FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Services/GringottsBank.Api/GringottsBank.Api.csproj", "src/Services/GringottsBank.Api/"]
RUN dotnet restore "src/Services/GringottsBank.Api/GringottsBank.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/GringottsBank.Api"
RUN dotnet build "GringottsBank.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GringottsBank.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GringottsBank.Api.dll"]