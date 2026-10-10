<script setup>
</script>

<template>
  <main>
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
</template>

<style scoped>
main {
  padding: 16px;
}
label {
  font-weight: 600;
}
input {
  width: 100%;
  max-width: 320px;
  padding: 6px 8px;
  box-sizing: border-box;
}
</style>