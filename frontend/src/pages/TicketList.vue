<script setup>
import { items } from '../ticketsStore.js'
import TicketTable from '../components/TicketTable.vue'
</script>

<template>
  <main>
    <h2>Заявки на ремонт оборудования</h2>
    <TicketTable :items="items" />
  </main>
</template>

<style scoped>
main {
  padding: 16px;
}
</style>