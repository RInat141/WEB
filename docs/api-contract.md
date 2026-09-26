# API-контракт

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

### POST /api/tickets — запрос

```json
[
  {
    "id": 101,
    "number": "T-2026-0101",
    "title": "Стук в передней подвеске",
    "status": "New",
    "siteId": 12,
    "assigneeUserId": null
  },
  {
    "id": 102,
    "number": "T-2026-0102",
    "title": "Замена тормозных колодок",
    "status": "InProgress",
    "siteId": 7,
    "assigneeUserId": 5
  }
]
