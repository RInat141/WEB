<script setup>
</script>

<template>
  <header>
    <h1>Учёт системного оборудования</h1>
    <nav>
      <router-link to="/">Список</router-link>
      <router-link to="/new">Создать</router-link>
      <router-link to="/login">Вход</router-link>
    </nav>
  </header>
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
nav a {
  margin-right: 12px;
  text-decoration: none;
  color: #1a4d8f;
}
nav a.router-link-active {
  font-weight: 700;
  text-decoration: underline;
}
</style>