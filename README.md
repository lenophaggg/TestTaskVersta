## Описание
Простое Web-приложение для приёма заказов на доставку с возможностью создания, просмотра и отображения списка заказов.

### Функционал:
1. **Создание заказа** (обязательные поля):
   - Город отправителя
   - Адрес отправителя
   - Город получателя
   - Адрес получателя
   - Вес груза
   - Дата забора груза
2. **Отображение списка заказов** с автоматическим номером заказа.
3. **Просмотр заказа** в режиме чтения по клику на заказ.
---

## Технологический стек
- **Backend**: ASP.NET 9 + EF
- **Frontend**: ASP.NET MVC
- **База данных**: PostgreSQL
---

## Запуск проекта

### 1. Клонирование репозитория
```sh
git clone https://github.com/your-username/unior-delivery-app.git
cd unior-delivery-app
```

### 2. Настройка базы данных
Заменитье строку подключения в appsettings.json
```csharp
"ConnectionStrings": {
    "ordersDbConnection": "Host=localhost;Port=5488;Database=ordersapp;Username=postgres;Password=228228228"
}
```


### 3. Импорт базы данных
```sh
psql -U postgres -d ordersapp -f "~/WebApplication2/Database/ordersDb.sql"
```


### 4. Сборка и запуск
```sh
cd WebApplication2
dotnet restore
dotnet build
dotnet run
```
После запуска приложение будет доступно по адресу:
[http://localhost:5000](http://localhost:5000)
[https://localhost:5001](https://localhost:5001)

