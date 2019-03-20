import Basket from '../Basket.vue';
export default {
  components: {
    Basket
  },
  created() {
    /**
     * using function set canteen
     */
    this.setCanteen();
  },
  mounted() {
    /**
     * create slider for price and calories filter
     */
    this.setSlider("#rub-left", "#rub-right", "#slider-range", 0, 1000);
    this.setSlider("#cal-left", "#cal-right", "#calory-range", 0, 1000);
  },
  methods: {
    setCanteen: function() {
      /**
       * finding active canteen
       */
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
      /**
       * get category meals for active canteen
       */
      this.$http.get(this.backUrl+'/api/canteens/'+this.canteen.id+'/categories').then(response => {
        this.categories = response.body
        this.getContent();
      })
    },
    /**
     * function create slider for price and calories filter
     */
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
          $(lCount).text(ui.values[0] + postfix); // text left count
          $(rCount).text(ui.values[1] + postfix); // text right count
          this.setPriceOrCaloriesFilter(ui.values[0], ui.values[1], active, minSize, maxSize)
        }
      });
      $(lCount).text($(bSlider).slider("values", 0) + postfix);
      $(rCount).text($(bSlider).slider("values", 1) + postfix);
    },
    /**
     * set values for categories in filters  to update content
     */
    setCategoryFilter: function(category) {
      if (this.filters.categories.indexOf(category) !== -1) {
        this.filters.categories.splice(this.filters.categories.indexOf(category), 1)
      } else {
        this.filters.categories.push(category)
      }
      /**
       * using function update content
       */
      this.updateContent()
    },
    /**
     * set values for price or calories for filters to update content
     */
    setPriceOrCaloriesFilter: function(min, max, active, minSize, maxSize) {
      if (active == "price") {
        this.filters.price = min == minSize && max == maxSize ? null : [min, max]
      } else if (active == "calories") {
        this.filters.kkal = min == minSize && max == maxSize ? null : [min, max]
      }
      /**
       * using function update content
       */
      this.updateContent()
    },
    /**
     * get content for start page
     */
    getContent: function() {
      /**
       * using function update content
       */
      this.updateContent()
    },
    /**
     * function for get cotent with filters
     */
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
        /**
         * using function create menu
         */
        this.createMenu()
      })
    },
    /**
     * function create menu for parsing in views
     */
    createMenu: function() {
      this.menu = []
      for (let category of this.categories){
        let _meals = []
        for (let meal of this.meals) {
          if (meal.mealCategoryId == category.id) {
            meal.type = "noBasket"
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
      if (this.basketMeals.length > 0) {
        for (let item of this.menu) {
          for (let meal of item.meals) {
            for (let basketMeal of this.basketMeals) {
              if (meal.id == basketMeal.id) {
                meal.type = 'isBasket'
              }
            }
          }
        }
      }
    },
    /**
     * function added meal to basket
     */
    addToBascket: function(meal) {
      if (this.basketMeals.indexOf(meal) !== -1) {
        meal.type = "noBasket"
        this.basketMeals.splice(this.basketMeals.indexOf(meal), 1)
      } else {
        for (let basketMeal of this.basketMeals) {
          if (basketMeal.id == meal.id) {
            meal.type = "noBasket"
            this.basketMeals.splice(this.basketMeals.indexOf(basketMeal), 1)
            return null
          }
        }
        meal.type = "isBasket"
        this.basketMeals.push(meal)
      }
    }
  },
  data() {
    /*
      global BACK_URL
    */
   /**
    * variable definition
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