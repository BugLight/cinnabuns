<template>
  <div class="main">
    <div class="canteen-container">
      <div class="canteen" v-for="canteen in canteens" :key="canteen.id" @click="linkToCanteen(canteen)">
        <div class="c-name">{{canteen.name}}</div>
        <div class="c-description">{{canteen.description}}</div>
        <div class="c-card" title="Оплата картой" v-if="canteen.acceptCards"><font-awesome-icon icon="credit-card" size="lg" :style="{ color: 'black' }"/> Оплата картой</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  created() {
    /**
     * get canteens for home page
     */
    this.$http.get(this.backUrl+'/api/canteens').then(response => {
      /**
       * recorde in global storage
       */
      this.$store.commit('setCanteens', response.body);
      this.canteens = this.$store.state.canteens;
    })
  },
  data() {
    /*
      global BACK_URL
    */
    return {
      canteens: null,
      backUrl: BACK_URL
    }
  },
  methods: {
    /**
     * route to canteen page and set active canteen
     */
    linkToCanteen: function (canteen) {
      this.$store.commit('setActiveCanteen', canteen)
      this.$router.push({name: 'canteen', params: {id: canteen.id}})
    }
  },
}
</script>
<style lang="scss">
.main {
  width: 100%;
}
.canteen-container {
  width: 50%;
  margin: 0 auto;
  @media (max-width: 900px) {
    width: 90%;
    margin: 20px auto;
  }
  .canteen { 
    min-height: 100px;
    border-radius: 5px;
    border: 2px solid black;
    margin: 10px 0;
    padding: 0 30px;
    position: relative;
    cursor: pointer;
    @media (max-width: 900px) {
      margin: 30px 0;
    }
    .c-name {
      font-size: 30px;
      @media (max-width: 900px) {
        font-size: 40px;
      }
    }
    .c-description {
      font-size: 23px;
      width: 500px;
      word-wrap: break-word;
      @media (max-width: 900px) {
        font-size: 33px;
      }
    }
    .c-card {
      position: absolute;
      top: 20px;
      right: 30px;
      @media (max-width: 900px) {
        font-size: 25px;
      }
    }
  }
}
</style>


