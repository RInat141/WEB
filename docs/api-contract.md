# API-контракт

Предметная область: заявки на ремонт и обслуживание системного оборудования
(компьютеры, серверы, сетевое оборудование).

Клиент **не присылает** при создании: `id`, `number`, `status`, `assigneeUserId` —
их определяет сервер.

## Таблица методов

| Метод и путь | Тело | Успех | Ошибка |
|---|---|---|---|
| GET /api/tickets | — | 200, массив или `[]` | — |
| GET /api/tickets/{id} | — | 200, объект | 404 |
| POST /api/tickets | title, siteId, description | 201, `{ id, number, status: "New" }` | 400 |
| PATCH /api/tickets/{id}/assignee | assigneeUserId | 200 | 404, 409 |
| PATCH /api/tickets/{id}/status | status | 200 | 404, 409 |

## Примеры JSON

### GET /api/tickets — ответ 200

```json
[
  {
    "id": 101,
    "number": "T-2026-0101",
    "title": "Не запускается сервер",
    "status": "New",
    "siteId": 12,
    "assigneeUserId": null
  },
  {
    "id": 102,
    "number": "T-2026-0102",
    "title": "Замена SSD в рабочей станции",
    "status": "InProgress",
    "siteId": 7,
    "assigneeUserId": 5
  }
]
```
# GET /api/tickets/{id} — ответ 200
```json
{
  "id": 101,
  "number": "T-2026-0101",
  "title": "Не запускается сервер",
  "description": "При включении нет POST, вентиляторы не вращаются",
  "status": "New",
  "siteId": 12,
  "createdByUserId": 3,
  "assigneeUserId": null,
  "createdAt": "2026-09-20T10:15:00Z",
  "updatedAt": "2026-09-20T10:15:00Z"
}

```
# GET /api/tickets/{id} — ошибка 404
```json
{ "error": "Ticket not found" }
```
# POST /api/tickets — запрос

```json
{
  "title": "Не запускается сервер",
  "siteId": 12,
  "description": "При включении нет POST, вентиляторы не вращаются"
}
```

# POST /api/tickets — ответ 201
```json
{
  "id": 101,
  "number": "T-2026-0101",
  "status": "New"
}
```

# POST /api/tickets — ошибка 400
```json
{ "error": "title is required" }
```

# PATCH /api/tickets/101/assignee — запрос
```json
{ "assigneeUserId": 5 }
```

# PATCH /api/tickets/101/assignee — ответ 200
```json
{
  "id": 101,
  "assigneeUserId": 5
}
```

# PATCH /api/tickets/101/status — запрос
```json
{ "status": "InProgress" }
```

# Статусы
Только: New, InProgress, Closed, Cancelled.
Отмена — Cancelled, а не удаление строки. Метода DELETE нет.
