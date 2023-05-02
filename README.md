> Не способен самостоятельно написать тестовое - не подавайся на стажировку

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

## Исправления по замечаниям

#### Ошибки в критериях:  не соблюден роутинг для работы с заказами, нельзя изменить кол-во товара по строке заказа. Если удалить заказ и создать указав id удаленного заказа будет необработанная ошибка (500). 

1. Переделал с /routes на /orders (не понял даже, почему сразу правильно не назвал...)
2. Товары и их количество теперь меняется корректно.
3. При создании заказа с удалённым Id вылетит ошибка, что данный ид зарезервирован с системе.

#### Доменные объекты завязаны на API-слой (атрибуты сериализации). Dto из API-слоя могут использоваться для работы с доменными объектами, но доменные объекты не должны “знать” как их будут использовать в DAL или API. То есть из доменного слоя вынести все знания о сериализации, а dto для контроллеров поместить в API. 

1. Вынес дтошки в Api
2. Атрибуты валидации не должны были быть вовсе, т.к везде использовались ДТО, ненужные свойства попросту бы не смогли попасть куда не нужно.

#### Нет user-friendly текстов ошибки для: проверки пустых строк при создании заказа, парсинга статуса при редактировании заказа, тексты ошибок отдаются в unicode.

1. Для проверки пустых строк, к сожалению, не понял как изменить ответ, т.к конвертация в гуид не падает в глобальную обработку.
2. При изменении статуса добавил проверку на корректность и ошибку в случае некорректного статуса
3. Добавил options при сериализации модели ответа в ответе. Теперь текста ошибок отдаются на русском без юникода


> p.s: LineController существует исключительно для предоставления idшников для тестов




