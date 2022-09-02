FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src 

COPY ["src/OzonEduStockApi/OzonEduStockApi.csproj", "src/OzonEduStockApi/"]

RUN dotnet restore "src/OzonEduStockApi/OzonEduStockApi.csproj"

COPY . . 

WORKDIR "/src/src/OzonEduStockApi"

RUN dotnet build "OzonEduStockApi.csproj" -c Release -o /app/build

FROM build AS publish 

RUN dotnet publish "OzonEduStockApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime 

WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM runtime AS final 
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OzonEduStockApi.dll"]

