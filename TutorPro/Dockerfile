FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
USER root
#For running docker container
RUN apt-get update && apt-get install -y sudo && apt-get install -y libgssapi-krb5-2
RUN apt-get clean
USER app
WORKDIR /app
EXPOSE 32793

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
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

USER root
FROM base AS final
WORKDIR /app

RUN chown -R app /app
# Added to set access rights
RUN chmod -R 755 /app
USER app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TutorPro.dll"]