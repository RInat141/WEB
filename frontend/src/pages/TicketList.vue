<script setup>
import { ref, onMounted } from 'vue'
import { getTickets } from '../api/tickets.js'
import { items } from '../ticketsStore.js'
import TicketTable from '../components/TicketTable.vue'

const screen = ref('loading') // 'loading' | 'empty' | 'data' | 'error'
const errorMessage = ref('')

async function load() {
  screen.value = 'loading'
  errorMessage.value = ''
  try {
    const list = await getTickets()
    items.value = list
    screen.value = list.length === 0 ? 'empty' : 'data'
  } catch (e) {
    errorMessage.value = describeError(e)
    screen.value = 'error'
  }
}

function describeError(e) {
  if (e.status === 0) return 'Нет связи с сервером'
  if (e.status === 400) return 'Проверьте поля'
  if (e.status === 500) return 'Сервер не смог обработать запрос'
  return `Ошибка: ${e.message || 'неизвестно'}`
}

onMounted(load)
</script>

<template>
  <main>
    <h2>Заявки на ремонт оборудования</h2>

    <p v-if="screen === 'loading'">Загрузка…</p>

    <p v-else-if="screen === 'empty'">Записей пока нет</p>

    <TicketTable v-else-if="screen === 'data'" :items="items" />

    <div v-else-if="screen === 'error'">
      <p style="color: #b00020">{{ errorMessage }}</p>
      <button type="button" @click="load">Повторить</button>
    </div>
  </main>
</template>

<style scoped>
main {
  padding: 16px;
}
</style>