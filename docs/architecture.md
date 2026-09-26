# Архитектура

<img width="932" height="251" alt="2131" src="https://github.com/user-attachments/assets/e1b597aa-b80c-4427-9574-9a4edb28bbb5" />

**архитектура web-ИС семестра**

Прямой стрелки от браузера к PostgreSQL нет — иначе клиент получил бы
доступ к данным и обошёл серверные правила. Вся валидация выполняется
на ASP.NET Core.
