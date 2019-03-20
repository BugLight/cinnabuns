# Cinnabuns

## Frontend

### Стэк технологий

Клиент разработан с использованием новых технологий. В разработке мы использовали babel транслятор для модульного JS и Vuejs.
Для сборки frontend используется webpack.

### Структура проекта

```
+-- frontend
  +--static
  +--src
  | `--components
  |   `--styles
  |   `--scripts
  `--dist
```

* `static` попка для статических файлов, изображений, frontend
* JavaScript млодули распологаются в папке `src`, компоненты Vuejs находятся в `src/components`, отдельные скрипты находятся в папке `src/components/scripts`, а стили в папке `/src/components/styles`
* `dist` содержит собранные файлы 

### Установка

Проект использует nodejs и webpack для сборки. Собранные файлы складываются в папку `dist`.

Для установки зависимостей в папке frontend (так же для выполнения следующих команд нужно перейти в папку fronend) необходимо использовать комманду `npm install`

Установить переменную окружения `BACK_URL=https://cinnabuns-chipper-chipmunk.eu-gb.mybluemix.net`

* Затем использовать `npm run prod` или `npm run build` для сборки проекта в режиме production или development
* Для запуска сервера необходимо использовать команду `npm run serve`

## Backend

Backend разработан с использованием серверного фреймворка ASP.NET Core и реализует архитектурный паттерн MVC.
Веб-хост приложения конфигурируется в классе Startup.

Так же включена поддержка средств виртуализации Docker.

В качестве хранилища данных используется СУБД MySQL. Основные сущности:

* Meal - блюдо
* MealCategory - категория блюд
* Canteen - столовая
* User - пользователь (администратор)
* Credentials - данные пользователя для аутентификации

Для сборки можно использовать консольную утилиту `dotnet`, либо средства IDE Visual Studio.

```
dotnet restore
dotnet build
dotnet run --server.urls http://*:80
```


## Команда

* semech
* buglight
* mesuchan
* kachalov