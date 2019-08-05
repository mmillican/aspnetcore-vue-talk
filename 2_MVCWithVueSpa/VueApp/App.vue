<template>
<div>
    <h1>Admin</h1>
    <admin-nav />
    
    <div class="app">
      <router-view></router-view>
    </div>
</div>
</template>
<script>

import AdminNav from '@/components/AdminNav';

export default {
  components: {
    AdminNav
  }  
}
</script>

<style scoped>
.app {border: red; }
</style>
