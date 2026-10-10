<script setup>
import { ref } from 'vue'

const draft = ref({
  title: '',
  siteId: '',
  description: ''
})
</script>

<template>
  <main>
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
</style>