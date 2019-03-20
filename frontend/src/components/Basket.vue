<template>
  <div class="basket">
    <div class="basket-false" @click="showBasket = !showBasket">
      <font-awesome-icon icon="shopping-basket" size="lg"/> Ваша корзина: {{price}} р. {{calories}} ккал.
    </div>
    <div class="basket-show-panel" v-if="showBasket">
      Блюда в корзине<br/>
      <div class="meals-container">
        {{!(meals.length > 0) ? 'пусто...' : null}}
        <ol>
          <li v-for="meal in meals" :key="meal.id">{{meal.name}}<br/><span style="font-size: 16px">Цена: {{meal.price}} р.<br/>Калорийность: {{meal.calorie}} ккал.</span></li>
        </ol>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  /**
   * props data for basket
   */
  props: {
    meals: {
      type: Array,
      default: null
    }
  },
  watch: {
    meals: function() {
      /**
       * using function for calculate and recalculate basket
       */
      this.calculateMeals()
    }
  },
  methods: {
    calculateMeals: function() {
      let _price = 0;
      let _calories = 0;
      for(let meal of this.meals) {
        _price += Number(meal.price)
        _calories += Number(meal.calorie)
      }
      this.price = _price
      this.calories = _calories
    }
  },
  data() {
    return {
      showBasket: false,
      price: 0,
      calories: 0
    }
  },
}
</script>

<style lang="scss" scoped>
.basket {
  position: fixed;
  bottom: 0;
  right: 30px;
  width: 300px;
  @media (max-width: 900px) {
    right: 0px;
    width: 100%;
  }
  .basket-false {
    width: 100%;
    border: 1px solid black;
    border-top-left-radius: 20px;
    padding: 10px 20px;
    cursor: pointer;
    background-color: #fff;
    @media (max-width: 900px) {
      border-top-right-radius: 20px;
      width: calc(100% - 40px);
    }
  }
  .basket-show-panel {
    width: 100%;
    padding: 10px 20px;
    border-left: 1px solid black;
    animation: showBasket .1s ease-in-out;
    background-color: #fff;
    .meals-container {
      padding-left: 20px;
      .reset-basket {
        cursor: pointer;
        padding: 5px;
        border-radius: 5px;
        border: 1px solid black;
        margin: 5px;
        width: auto;
        text-align: center;
        &:hover {
          background-color: #c4c4c5
        }
      }
      ol {
        padding-left: 0;
      }
    }
  }
}

@keyframes showBasket {
  0% {
    height: 0
  }
  100% {
    height: auto;
  }
}
</style>

