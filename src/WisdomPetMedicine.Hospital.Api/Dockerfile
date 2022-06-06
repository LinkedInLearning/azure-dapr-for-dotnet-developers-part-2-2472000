#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WisdomPetMedicine.Hospital.Api/WisdomPetMedicine.Hospital.Api.csproj", "WisdomPetMedicine.Hospital.Api/"]
COPY ["WisdomPetMedicine.Hospital.Domain/WisdomPetMedicine.Hospital.Domain.csproj", "WisdomPetMedicine.Hospital.Domain/"]
COPY ["WisdomPetMedicine.Common/WisdomPetMedicine.Common.csproj", "WisdomPetMedicine.Common/"]
COPY ["WisdomPetMedicine.Hospital.Infrastructure/WisdomPetMedicine.Hospital.Infrastructure.csproj", "WisdomPetMedicine.Hospital.Infrastructure/"]
RUN dotnet restore "WisdomPetMedicine.Hospital.Api/WisdomPetMedicine.Hospital.Api.csproj"
COPY . .
WORKDIR "/src/WisdomPetMedicine.Hospital.Api"
RUN dotnet build "WisdomPetMedicine.Hospital.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WisdomPetMedicine.Hospital.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WisdomPetMedicine.Hospital.Api.dll"]
