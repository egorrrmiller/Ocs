## Стек:
- .NET 7
- ASP.NET Core Web Api
- PostgreSQL
- Swagger

## Для запуска

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

<https://localhost:7179/swagger/index.html>

Миграции создавать не надо, они создадутся и применятся автоматически

## Комментарии
1. Спустя час-два размышления, была сделана все таки база Many-to-Many, т.к заказов с одним и тем же товаром может быть несколько.<br/>
Также сделал контроллер для добавления товаров. С Qty у Product можно придумать что-нибудь с учётом количества товара на складах и т.п
> Понимаю, что не требовалось, но в моём понимании, работа с заказами, где могут быть одинаковые товары в разных заказах, не может быть реализована на one-to-many связи, как требовалось по ТЗ. По опыту прошлых выполненных тестовых, я решил все таки сделать Many-to-Many.

2. Из за данного пункта сделал в базе доп таблицу Deleted, которая бы помечала удаленные заказы<br/>
![image](https://user-images.githubusercontent.com/44502536/234329730-837ca37e-e389-4be5-bdb4-81920a8580d3.png)

3. Похожие тестовые задания, которые я выполнял: 
- https://github.com/egorrrmiller/Modsen
- https://github.com/egorrrmiller/kaspel (здесь Many-To-Many)



