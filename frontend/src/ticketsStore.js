import { ref } from 'vue'

// Общий список заявок для UI.
// Наполняется через API-модуль (getTickets / createTicket).
// После F5 данные исчезнут — серверное хранилище появится позже.
export const items = ref([])