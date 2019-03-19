FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["backend.csproj", "backend/"]
RUN dotnet restore "backend/backend.csproj"
WORKDIR "/src/backend"
COPY . .
RUN dotnet build "backend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "backend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "backend.dll"]