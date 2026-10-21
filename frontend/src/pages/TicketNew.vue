<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { createTicket } from '../api/tickets.js'
import { items } from '../ticketsStore.js'

const router = useRouter()

const form = ref({
  title: '',
  siteId: 0,
  description: ''
})

const errors = ref({ title: '', siteId: '', description: '' })
const submitError = ref('')
const isSubmitting = ref(false)

function validate() {
  errors.value = { title: '', siteId: '', description: '' }
  let ok = true

  const title = form.value.title.trim()
  if (title.length < 5 || title.length > 80) {
    errors.value.title = 'Тема: от 5 до 80 символов'
    ok = false
  }

  const siteId = form.value.siteId
  if (!Number.isInteger(siteId) || siteId <= 0) {
    errors.value.siteId = 'Участок: целое число больше 0'
    ok = false
  }

  const description = form.value.description.trim()
  if (description.length < 10 || description.length > 500) {
    errors.value.description = 'Описание: от 10 до 500 символов'
    ok = false
  }

  return ok
}

async function onCreate() {
  if (isSubmitting.value) return
  if (!validate()) return

  isSubmitting.value = true
  submitError.value = ''

  try {
    const created = await createTicket({
      title: form.value.title.trim(),
      siteId: form.value.siteId,
      description: form.value.description.trim()
    })
    // созданный объект берём из ответа (id, number, status приходят с сервера)
    items.value.push(created)
    form.value = { title: '', siteId: 0, description: '' }
    errors.value = { title: '', siteId: '', description: '' }
    router.push('/')
  } catch (e) {
    submitError.value = describeError(e)
  } finally {
    isSubmitting.value = false
  }
}

function describeError(e) {
  if (e.status === 0) return 'Нет связи с сервером'
  if (e.status === 400) return 'Проверьте поля'
  if (e.status === 500) return 'Сервер не смог обработать запрос'
  return `Ошибка: ${e.message || 'неизвестно'}`
}
</script>

<template>
  <main>
    <h2>Новая заявка</h2>

    <form @submit.prevent="onCreate">
      <p>
        <label for="title">Тема (5–80 символов)</label><br />
        <input id="title" name="title" v-model="form.title" autofocus :disabled="isSubmitting" />
        <br />
        <small v-if="errors.title" style="color: #b00020">{{ errors.title }}</small>
      </p>

      <p>
        <label for="siteId">Участок (siteId, целое &gt; 0)</label><br />
        <input id="siteId" name="siteId" type="number" v-model.number="form.siteId" :disabled="isSubmitting" />
        <br />
        <small v-if="errors.siteId" style="color: #b00020">{{ errors.siteId }}</small>
      </p>

      <p>
        <label for="description">Описание (10–500 символов)</label><br />
        <textarea id="description" name="description" v-model="form.description" :disabled="isSubmitting"></textarea>
        <br />
        <small v-if="errors.description" style="color: #b00020">{{ errors.description }}</small>
      </p>

      <button type="submit" :disabled="isSubmitting">
        {{ isSubmitting ? 'Создаём…' : 'Создать' }}
      </button>

      <p v-if="submitError" style="color: #b00020">{{ submitError }}</p>
    </form>

    <p><small>Mock: данные живут только в памяти. F5 — исчезнут, это ожидаемо.</small></p>
  </main>
</template>

<style scoped>
main {
  padding: 16px;
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
small {
  display: inline-block;
  margin-top: 4px;
}
button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}
</style>