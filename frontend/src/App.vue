<script setup>
import { ref } from 'vue'

// 'list' | 'new' | 'login'
const screen = ref('list')

// Заготовка массива — две строки, чтобы таблица не была пустой.
// Статусы английские, как в ЛР2: New | InProgress | Closed | Cancelled
const items = ref([
  { id: 1, number: 'T-2026-0001', title: 'Не запускается сервер', siteId: 2, status: 'New' },
  { id: 2, number: 'T-2026-0002', title: 'Замена SSD в рабочей станции', siteId: 1, status: 'InProgress' }
])

// Черновик формы (пока без сохранения)
const draft = ref({
  title: '',
  siteId: '',
  description: ''
})
</script>

<template>
  <div>
    <header>
      <h1>Учёт системного оборудования</h1>
      <nav>
        <button type="button" @click="screen = 'list'">Список</button>
        <button type="button" @click="screen = 'new'">Создать</button>
        <button type="button" @click="screen = 'login'">Вход</button>
      </nav>
    </header>

    <!-- ================= СПИСОК ================= -->
    <main v-if="screen === 'list'">
      <h2>Заявки на ремонт оборудования</h2>

      <table>
        <thead>
          <tr>
            <th>Номер</th>
            <th>Участок (siteId)</th>
            <th>Тема</th>
            <th>Статус</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <td>{{ item.number }}</td>
            <td>{{ item.siteId }}</td>
            <td>{{ item.title }}</td>
            <td>{{ item.status }}</td>
          </tr>
        </tbody>
      </table>

      <p v-if="items.length === 0">Заявок пока нет.</p>
    </main>

    <!-- ================= ФОРМА ================= -->
    <main v-else-if="screen === 'new'">
      <h2>Новая заявка</h2>

      <form @submit.prevent>
        <p>
          <label for="title">Тема (5–80 символов)</label><br />
          <input id="title" name="title" v-model="draft.title" autofocus />
        </p>

        <p>
          <label for="siteId">Участок (siteId)</label><br />
          <input id="siteId" name="siteId" v-model="draft.siteId" />
        </p>

        <p>
          <label for="description">Описание (10–500 символов)</label><br />
          <textarea id="description" name="description" v-model="draft.description"></textarea>
        </p>

        <button type="submit">Создать</button>
      </form>

      <p><small>Проверку длины напишем в ЛР5. Сейчас кнопка ничего не сохраняет.</small></p>
    </main>

    <!-- ================= ВХОД ================= -->
    <main v-else>
      <h2>Вход</h2>

      <form @submit.prevent>
        <p>
          <label for="login">Логин</label><br />
          <input id="login" name="login" autocomplete="username" />
        </p>

        <p>
          <label for="password">Пароль</label><br />
          <input id="password" name="password" type="password" autocomplete="current-password" />
        </p>

        <button type="submit">Войти</button>
      </form>

      <p><small>Заглушка: настоящей проверки пока нет.</small></p>
    </main>
  </div>
</template>

<style scoped>
header {
  padding: 12px 16px;
  border-bottom: 1px solid #ccc;
}
h1 {
  margin: 0 0 8px;
  font-size: 20px;
}
nav button {
  margin-right: 8px;
}
main {
  padding: 16px;
}
table {
  border-collapse: collapse;
  width: 100%;
  max-width: 720px;
}
th,
td {
  border: 1px solid #ccc;
  padding: 6px 10px;
  text-align: left;
}
label {
  font-weight: 600;
}
input,
textarea {
  width: 100%;
  max-width: 480px;
  padding: 6px 8px;
  box-sizing: border-box;
}
textarea {
  min-height: 100px;
}
</style>