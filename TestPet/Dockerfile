FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestPet/TestPet.csproj", "TestPet/"]
COPY ["AnimeShop.EFDal/AnimeShop.EFDal.csproj", "AnimeShop.EFDal/"]
COPY ["AnimeShop.Common/AnimeShop.Common.csproj", "AnimeShop.Common/"]
COPY ["AnimeShop.Dal.Interfaces/AnimeShop.Dal.Interfaces.csproj", "AnimeShop.Dal.Interfaces/"]
COPY ["AnimeShop.Bll.Interfaces/AnimeShop.Bll.Interfaces.csproj", "AnimeShop.Bll.Interfaces/"]
COPY ["AnimeShop.Bll/AnimeShop.Bll.csproj", "AnimeShop.Bll/"]
RUN dotnet restore "TestPet/TestPet.csproj"
COPY . .
WORKDIR "/src/TestPet"
RUN dotnet build "TestPet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestPet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestPet.dll"]
