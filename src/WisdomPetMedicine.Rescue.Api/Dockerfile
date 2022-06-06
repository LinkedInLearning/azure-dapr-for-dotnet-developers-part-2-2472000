#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WisdomPetMedicine.Rescue.Api/WisdomPetMedicine.Rescue.Api.csproj", "WisdomPetMedicine.Rescue.Api/"]
COPY ["WisdomPetMedicine.Rescue.Domain/WisdomPetMedicine.Rescue.Domain.csproj", "WisdomPetMedicine.Rescue.Domain/"]
COPY ["WisdomPetMedicine.Common/WisdomPetMedicine.Common.csproj", "WisdomPetMedicine.Common/"]
RUN dotnet restore "WisdomPetMedicine.Rescue.Api/WisdomPetMedicine.Rescue.Api.csproj"
COPY . .
WORKDIR "/src/WisdomPetMedicine.Rescue.Api"
RUN dotnet build "WisdomPetMedicine.Rescue.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WisdomPetMedicine.Rescue.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WisdomPetMedicine.Rescue.Api.dll"]
