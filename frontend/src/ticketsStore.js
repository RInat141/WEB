import { ref } from 'vue'

// Единственный источник тестовых записей.
// После F5 модуль пересоздаётся — данные из памяти исчезнут. Это ожидаемо.
export const items = ref([
  { id: 1, number: 'T-2026-0001', title: 'Не запускается сервер', siteId: 2, status: 'New' },
  { id: 2, number: 'T-2026-0002', title: 'Замена SSD в рабочей станции', siteId: 1, status: 'InProgress' }
])

// Учебный счётчик id. В ЛР6 источник номера изменится на ответ API.
export const nextId = ref(3)

// Добавить заявку. Возвращает созданный объект.
export function addItem({ title, siteId, description }) {
  const id = nextId.value
  const item = {
    id,
    number: `TMP-${id}`,
    title,
    siteId,
    description,
    status: 'New'
  }
  items.value.push(item)
  nextId.value += 1
  return item
}