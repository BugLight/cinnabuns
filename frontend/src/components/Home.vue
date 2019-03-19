<template>
  <div class="main">
    <div class="canteen-container">
      
    </div>
  </div>
</template>

<script>
export default {
  created() {
    this.$http.get('http://172.17.0.3/api/canteens').then(response => {
      console.log(response.body)
    })
  }
}
</script>
<style lang="scss">
.main {
  width: 100%;
}
.canteen-container {
  width: 70%;
  margin: 0 auto;
}
</style>


