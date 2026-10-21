// Единый API-модуль. Страницы импортируют только эти две функции
// и не знают, откуда берутся данные: mock или реальный fetch.

import { mockGetTickets, mockCreateTicket } from './mockTickets.js'

// Переключатель: true — mock (без backend), false — реальный fetch.
export const USE_MOCK = false

const baseUrl = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000'

// --- getTickets() → Promise<Array> ---
export async function getTickets() {
  if (USE_MOCK) {
    const res = await mockGetTickets()
    if (!res.ok) throw makeError(res.status, res.data)
    return res.data
  }

  let response
  try {
    response = await fetch(`${baseUrl}/api/tickets`)
  } catch (e) {
    throw makeError(0, { error: 'Нет связи с сервером' })
  }
  if (!response.ok) {
    const data = await safeJson(response)
    throw makeError(response.status, data)
  }
  return response.json()
}

// --- createTicket(body) → Promise<объект> ---
export async function createTicket(body) {
  if (USE_MOCK) {
    const res = await mockCreateTicket(body)
    if (!res.ok) throw makeError(res.status, res.data)
    return res.data
  }

  let response
  try {
    response = await fetch(`${baseUrl}/api/tickets`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    })
  } catch (e) {
    throw makeError(0, { error: 'Нет связи с сервером' })
  }
  if (!response.ok) {
    const data = await safeJson(response)
    throw makeError(response.status, data)
  }
  return response.json()
}

// --- вспомогательное ---
function makeError(status, data) {
  const err = new Error((data && data.error) || `HTTP ${status}`)
  err.status = status
  err.data = data
  return err
}

async function safeJson(response) {
  try {
    return await response.json()
  } catch {
    return {}
  }
}