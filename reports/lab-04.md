## Что сделано

- Подключён `vue-router`, три маршрута: `/`, `/new`, `/login`.
- Шапка вынесена в `AppHeader.vue` и стоит снаружи `router-view`.
- Таблица вынесена в `TicketTable.vue` и получает `items` через props.
- Массив заявок перенесён в `src/ticketsStore.js` — единственный источник.

## Схема App.vue

App.vue
├── AppHeader ← всегда виден
└── router-view ← текущая страница

## Скриншоты

![Главная страница](image.png)
![страница /new](image-1.png)
![страница /login](image-2.png)
![Дерево /src](image-3.png)

## Данные

Таблица получает массив через props, источник — `ticketsStore.js`.