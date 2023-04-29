FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Установите рабочую папку
WORKDIR /app

COPY ./Backend/DatabaseManager .

COPY ./Nugets ./PmSim.Backend.DatabaseManager/.nugets

RUN dotnet restore -s ./PmSim.Backend.DatabaseManager/.nugets -s https://api.nuget.org/v3/index.json

RUN dotnet publish -c Release -o out

# Создайте временный образ на основе официального образа .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Установите рабочую папку
WORKDIR /app

# Копируйте исполняемые файлы из временного образа сборки
COPY --from=build /app/out .

EXPOSE 80
# Запуск приложения
ENTRYPOINT ["dotnet", "PmSim.Backend.DatabaseManager.dll"]