// Учебный mock: имитирует сервер без backend.
// Задержка ~500 мс, чтобы UI успел показать «Загрузка…».

let nextId = 3

let store = [
  { id: 1, number: 'T-2026-0001', title: 'Не запускается сервер', siteId: 2, description: 'При включении нет POST', status: 'New' },
  { id: 2, number: 'T-2026-0002', title: 'Замена SSD в рабочей станции', siteId: 1, description: 'Диск сыпется, нужна замена', status: 'InProgress' }
]

function delay(ms) {
  return new Promise((resolve) => setTimeout(resolve, ms))
}

// getTickets() → { ok, status, data }
export async function mockGetTickets() {
  await delay(500)
  return { ok: true, status: 200, data: [...store] }
}

// createTicket(body) → { ok, status, data } либо { ok: false, status, data: { error } }
export async function mockCreateTicket(body) {
  await delay(500)

  // Учебный флаг: force500 в body → 500
  if (body && body.force500) {
    return { ok: false, status: 500, data: { error: 'Internal error (mock)' } }
  }

  // Короткая тема → 400
  const title = (body.title || '').trim()
  if (title.length < 5) {
    return { ok: false, status: 400, data: { error: 'title is too short' } }
  }

  // siteId должен быть числом > 0
  if (!Number.isInteger(body.siteId) || body.siteId <= 0) {
    return { ok: false, status: 400, data: { error: 'siteId must be > 0' } }
  }

  // Нормальное создание → 201
  const id = nextId++
  const created = {
    id,
    number: `T-2026-${String(id).padStart(4, '0')}`,
    title,
    siteId: body.siteId,
    description: (body.description || '').trim(),
    status: 'New'
  }
  store.push(created)
  return { ok: true, status: 201, data: created }
}