# Используйте официальный образ Microsoft .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Установите рабочую папку
WORKDIR /app

# Копируйте файлы проекта и восстановите зависимости
COPY *.sln ./
COPY Backend Backend/
COPY Frontend Frontend/
COPY Shared Shared/


RUN dotnet restore

# Скопируйте все остальные файлы и скомпилируйте проект
COPY . ./
RUN dotnet publish -c Release -o out

# Создайте временный образ на основе официального образа .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Установите рабочую папку
WORKDIR /app

# Копируйте исполняемые файлы из временного образа сборки
COPY --from=build-env /app/out .

# Запуск приложения
ENTRYPOINT ["dotnet", "PmSim.Backend.Gateway.dll"]
