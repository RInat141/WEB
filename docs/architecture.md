# Архитектура

<img width="932" height="251" alt="2131" src="https://github.com/user-attachments/assets/e1b597aa-b80c-4427-9574-9a4edb28bbb5" />

**архитектура web-ИС семестра**

Клиент(fronted) **Vue 3 + Vite** — `localhost:5173`.
Сервер(backend) **ASP.NET Core** — `localhost:5000`.
База **PostgreSQL** — обычно `:5432`.

Прямой стрелки от браузера к PostgreSQL нет — иначе клиент получил бы
доступ к данным и обошёл серверные правила. Вся валидация выполняется
на ASP.NET Core.
