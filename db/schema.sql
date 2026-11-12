-- Безопасное пересоздание таблиц (сначала удаляем в правильном порядке)
DROP TABLE IF EXISTS tickets CASCADE;
DROP TABLE IF EXISTS sites CASCADE;
DROP TABLE IF EXISTS users CASCADE;

-- 1. Пользователи
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    full_name TEXT NOT NULL,
    role TEXT NOT NULL CHECK (role IN ('Dispatcher', 'Engineer'))
);

-- 2. Справочник (оборудование)
CREATE TABLE sites (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    kind TEXT NOT NULL,
    serial TEXT NOT NULL UNIQUE
);

-- 3. Главный объект (Заявка)
CREATE TABLE tickets (
    id SERIAL PRIMARY KEY,
    number TEXT NOT NULL UNIQUE,
    title TEXT NOT NULL,
    description TEXT,
    status TEXT NOT NULL CHECK (status IN ('New', 'InProgress', 'Closed', 'Cancelled')),
    site_id INT NOT NULL REFERENCES sites(id),
    created_by_user_id INT NOT NULL REFERENCES users(id),
    assignee_user_id INT REFERENCES users(id), -- допускает NULL
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

-- Индексы (ускоряют фильтрацию по статусу и поиск по оборудованию)
CREATE INDEX tickets_status_idx ON tickets (status);
CREATE INDEX tickets_site_id_idx ON tickets (site_id);