# Матрица требований

Соответствие критериев ЛР1 полям модели и HTTP-запросам API.

| Требование ЛР1 | Поле или сущность | Запрос | Критерий |
|---|---|---|---|
| Создать заявку на ремонт оборудования | Ticket, title, siteId, description | POST /api/tickets | 1 |
| Назначить исполнителя | assigneeUserId | PATCH /api/tickets/{id}/assignee | 2 |
| Перевести в работу | status | PATCH /api/tickets/{id}/status | 3 |
