import Basket from '../Basket.vue';
export default {
  components: {
    Basket
  },
  created() {
    this.setCanteen();
  },
  mounted() {
    this.setSlider("#rub-left", "#rub-right", "#slider-range", 0, 1000);
    this.setSlider("#cal-left", "#cal-right", "#calory-range", 0, 1000);
  },
  methods: {
    setCanteen: function() {
      if(this.$store.state.activeCanteen) {
        this.canteen = this.$store.state.activeCanteen;
        this.getCategories();
      } else {
        this.$http.get(this.backUrl+'/api/canteens/'+this.$route.params.id).then(response => {
          this.$store.commit('setActiveCanteen', response.body)
          this.canteen = response.body
          this.getCategories();
        })
      }
    },
    getCategories: function() {
      this.$http.get(this.backUrl+'/api/canteens/'+this.canteen.id+'/categories').then(response => {
        this.categories = response.body
        this.getContent();
      })
    },
    setSlider: function(lCount, rCount, bSlider, minSize, maxSize) {
      let postfix = bSlider == '#slider-range' ? ' p.' : ' ккал.';
      let active = bSlider == '#slider-range' ? 'price' : 'calories';
      $(bSlider).slider({
        range: true,
        min: 0,
        max: maxSize,
        values: [minSize, maxSize],
        step: 10,
        slide: (event, ui) => {
          $(lCount).text(ui.values[0] + postfix); // текст левого count
          $(rCount).text(ui.values[1] + postfix); // текст правого count
          this.setPriceOrCaloriesFilter(ui.values[0], ui.values[1], active, minSize, maxSize)
        }
      });
      // задать начальный текст левого count
      $(lCount).text($(bSlider).slider("values", 0) + postfix);
      // задать начальный текст правого count
      $(rCount).text($(bSlider).slider("values", 1) + postfix);
    },
    setCategoryFilter: function(category) {
      if (this.filters.categories.indexOf(category) !== -1) {
        this.filters.categories.splice(this.filters.categories.indexOf(category), 1)
      } else {
        this.filters.categories.push(category)
      }
      this.updateContent()
    },
    setPriceOrCaloriesFilter: function(min, max, active, minSize, maxSize) {
      if (active == "price") {
        this.filters.price = min == minSize && max == maxSize ? null : [min, max]
      } else if (active == "calories") {
        this.filters.kkal = min == minSize && max == maxSize ? null : [min, max]
      }
      this.updateContent()
    },
    getContent: function() {
      this.updateContent()
    },
    updateContent: function() {
      let categories = this.filters.categories,
          price = this.filters.price,
          calories = this.filters.kkal;
      this.$http.get(this.backUrl+`/api/canteens/${this.canteen.id}/meals?${
        categories.length > 0 ? `categories=${categories.join(',')}` : ''
      }${
        categories.length > 0 && price ? '&' : ''
      }${
        price ? `priceMin=${price[0]}&priceMax=${price[1]}` : ''
      }${
        calories && price ? '&' : categories.length > 0 && calories ? '&' : ''
      }${
        calories ? `caloriesMin=${calories[0]}&caloriesMax=${calories[1]}` : ''
      }`).then(response => {
        this.meals = response.body
        this.createMenu()
      })
    },
    createMenu: function() {
      this.menu = []
      for (let category of this.categories){
        let _meals = []
        for (let meal of this.meals) {
          if (meal.mealCategoryId == category.id) {
            _meals.push(meal)
          }
        }
        if (_meals.length > 0) {
          this.menu.push({
            category: category,
            meals: _meals
          })
        }
      }
    },
    addToBascket: function(meal, el) {
      if (this.basketMeals.indexOf(meal) !== -1) {
        $(el).html('Добавить в корзину')
        this.basketMeals.splice(this.basketMeals.indexOf(meal), 1)
      } else {
        $(el).html('Убрать из корзины')
        this.basketMeals.push(meal)

      }
    }
  },
  data() {
    /*
      global BACK_URL
    */
    return {
      basketMeals: [],
      categories: null,
      canteen: null,
      meals: null,
      backUrl: BACK_URL,
      menu: [],
      filters: {
        categories: [],
        price: null,
        kkal: null
      }
    }
  }
}