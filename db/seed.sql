-- Пользователи
INSERT INTO users (full_name, role) VALUES
('Иван Диспетчеров', 'Dispatcher'),
('Пётр Инженеров', 'Engineer');

-- Оборудование (Site)
INSERT INTO sites (name, kind, serial) VALUES
('Сервер БД', 'Server', 'SRV-001'),
('Рабочая станция 1', 'PC', 'PC-101');

-- Заявки (Ticket) — создаём 4 штуки, чтобы покрыть все 4 статуса
INSERT INTO tickets (number, title, description, status, site_id, created_by_user_id, assignee_user_id) VALUES
('T-2026-0001', 'Не запускается сервер', 'Нет POST', 'New', 1, 1, NULL),
('T-2026-0002', 'Замена SSD', 'Диск сыпется', 'InProgress', 2, 1, 2),
('T-2026-0003', 'Сгорел блок питания', 'Запах гари', 'Closed', 1, 2, 2),
('T-2026-0004', 'Ошибка сети', 'Нет пинга', 'Cancelled', 2, 1, NULL);