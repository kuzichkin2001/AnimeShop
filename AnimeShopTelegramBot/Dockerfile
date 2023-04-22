FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AnimeShopTelegramBot/AnimeShopTelegramBot.csproj", "AnimeShopTelegramBot/"]
COPY ["AnimeShop.Dal.Interfaces/AnimeShop.Dal.Interfaces.csproj", "AnimeShop.Dal.Interfaces/"]
COPY ["AnimeShop.Common/AnimeShop.Common.csproj", "AnimeShop.Common/"]
COPY ["AnimeShop.EFDal/AnimeShop.EFDal.csproj", "AnimeShop.EFDal/"]
RUN dotnet restore "AnimeShopTelegramBot/AnimeShopTelegramBot.csproj"
COPY . .
WORKDIR "/src/AnimeShopTelegramBot"
RUN dotnet build "AnimeShopTelegramBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AnimeShopTelegramBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimeShopTelegramBot.dll"]
