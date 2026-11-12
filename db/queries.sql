-- 6.1. Фильтр и сортировка
-- Получить все новые заявки, сначала самые свежие
SELECT number, title, status, created_at
FROM tickets
WHERE status = 'New'
ORDER BY created_at DESC;

-- 6.2. JOIN
-- Получить список заявок с названием оборудования
SELECT t.number, t.title, s.name AS site_name, t.status
FROM tickets AS t
JOIN sites AS s ON s.id = t.site_id
ORDER BY t.created_at DESC;

-- 6.3. GROUP BY
-- Посчитать количество заявок в каждом статусе
SELECT status, COUNT(*) AS count
FROM tickets
GROUP BY status
ORDER BY status;

-- 6.4. Проверка CHECK (для отчёта)
-- Попытка вставить запрещенный статус. Выдаст ошибку.
INSERT INTO tickets (number, title, status, site_id, created_by_user_id) 
VALUES ('T-2026-0005', 'Тест', 'Done', 1, 1);