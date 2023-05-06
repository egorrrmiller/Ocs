## Стек:
- .NET 7
- ASP.NET Core Web Api
- PostgreSQL
- Swagger

## Для запуска

**[Скачать и установить последнюю версию .NET SDK 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)**

Клонировать репозиторий

```bash
  git clone https://github.com/egorrrmiller/Ocs
```

Перейти в папку с проектом

```bash
  cd Ocs/Ocs.Api
```

Изменить строку подключения в appsettings.json

```json
"ConnectionStrings": {
    "Postgres": "User ID=<userId>;Password=<password>;Host=<host>;Port=<port>;Database=ocs;"
  }
```

Запустить проект

```bash
  dotnet run --project .\Ocs.Api\Ocs.Api.csproj --launch-profile https
```

После запуска переходим по ссылке на страницу сваггера

https://localhost:7179/swagger/index.html

[Миграция](https://github.com/egorrrmiller/Ocs/blob/main/Ocs.Database/Migrations/20230501201604_Init.cs) применится автоматически




