import { ref } from 'vue'

// Единственный источник тестовых записей.
// После F5 модуль пересоздаётся — это нормально для ЛР4.
// Серверное хранение появится позже.
export const items = ref([
  { id: 1, number: 'T-2026-0001', title: 'Не запускается сервер', siteId: 2, status: 'New' },
  { id: 2, number: 'T-2026-0002', title: 'Замена SSD в рабочей станции', siteId: 1, status: 'InProgress' }
])