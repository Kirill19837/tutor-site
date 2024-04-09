FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
RUN apt-get update && apt-get install -y sudo && apt-get install -y libgssapi-krb5-2
RUN apt-get clean
USER app
WORKDIR /app
EXPOSE 8090

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TutorPro/TutorPro.csproj", "TutorPro/"]
COPY ["TutorPro.Domain/TutorPro.Domain.csproj", "TutorPro.Domain/"]
COPY ["TutorPro.Application/TutorPro.Application.csproj", "TutorPro.Application/"]
RUN dotnet restore "./TutorPro/./TutorPro.csproj"
COPY . .
WORKDIR "/src/TutorPro"
RUN dotnet build "./TutorPro.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TutorPro.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN mkdir -p /app/umbraco/Data/TEMP

RUN chmod -R 777 /app/umbraco/Data/TEMP

USER app
ENTRYPOINT ["dotnet", "TutorPro.dll"]