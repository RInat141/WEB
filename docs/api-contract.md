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
{
  "title": "Стук в передней подвеске",
  "siteId": 12,
  "description": "При проезде неровностей слышен стук справа"
}
