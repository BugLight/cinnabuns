<template>
  <div class="main">
    <div class="content" v-if="isAuthenticated">
      <div class="modal" v-if="showModal">
        <a class="modal-close" v-on:click="closeModal">
          <font-awesome-icon icon="times" :style="{ color: '#666' }"/>
        </a>
        <label>Название</label>
        <div class="form-control">
          <input type="text" v-model="modalMealCopy.name">
        </div>
        <label>Категория</label>
        <div class="form-control">
          <select v-model="modalMealCopy.mealCategory">
            <option v-for="(category, i) in modalCanteen.categories" :key="i" v-bind:value="category">
              {{category.name}}
            </option>
          </select>
        </div>
        <label>Цена</label>
        <div class="form-control">
          <input type="number" v-model="modalMealCopy.price">
        </div>
        <label>Вес</label>
        <div class="form-control">
          <input type="number" v-model="modalMealCopy.weight">
        </div>
        <label>Калорийность</label>
        <div class="form-control">
          <input type="number" v-model="modalMealCopy.calorie">
        </div>
        <button class="button-submit button-form" v-on:click="confirmModal">Сохранить</button>
      </div>
      <h1>Панель администратора</h1>
      <button class="button-cancel" v-on:click="logout">Выйти</button>
      <div>
        <div v-for="(canteen, i) in canteens" :key="i">
          <h2>{{canteen.name}}</h2>
          <button class="button-submit" v-on:click="createMeal(canteen)">Добавить</button>
          <table>
            <tr>
              <th align="left">Название</th>
              <th align="left">Категория</th>
              <th align="left">Цена</th>
              <th align="left">Вес</th>
              <th align="left">Калорийность</th>
            </tr>
            <tr v-for="(meal, j) in canteen.meals" :key="i">
              <td>{{meal.name}}</td>
              <td>{{meal.mealCategory.name}}</td>
              <td>{{meal.price}}</td>
              <td>{{meal.weight}}</td>
              <td>{{meal.calorie}}</td>
              <td>
                <a v-on:click="editMeal(canteen, meal)">
                  <font-awesome-icon icon="edit" :style="{ color: '#666', cursor: 'pointer' }"/>
                </a>
              </td>
              <td>
                <a v-on:click="deleteMeal(i, j)">
                  <font-awesome-icon icon="times" :style="{ color: '#666', cursor: 'pointer' }"/>
                </a>
              </td>
            </tr>
          </table>
        </div>
      </div>
    </div>
    <div class="content" v-else>
      <h1>Выполните вход</h1>
      <label>Имя пользователя</label>
      <div class="form-control">
        <input type="text" v-model="name">
      </div>
      <label>Пароль</label>
      <div class="form-control">
        <input type="password" v-model="password">
      </div>
      <div class="form-control">
        <button class="button-submit button-form" v-on:click="login">Войти</button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  data () {
    return {
      name: '',
      password: '',
      canteens: [],
      showModal: false,
      modalCanteen: null,
      modalMeal: null,
      modalMealCopy: null,
      editing: false
    };
  },
  created() {
    this.loadMeals();
  },
  methods: {
    login () {
      this.$store.dispatch('auth', {
        name: this.name,
        password: this.password
      })
      .then(token => {
        alert('Вход выполнен успешно!');
      })
      .catch(e => {
        alert('Ошибка входа. Повторите попытку снова.');
        this.clearForm();
      });
    },
    logout () {
      this.$store.commit('clearAccessToken');
    },
    clearForm () {
      this.name = '';
      this.password = '';
    },
    loadMeals () {
      this.$http.get(BACK_URL + '/api/canteens')
        .then(response => response.json())
        .then(canteens => {
          return Promise.all(canteens.map(c => {
            return this.$http.get(BACK_URL + '/api/canteens/' + c.id.toString() + '/meals')
              .then(response => response.json())
              .then(meals => {
                c.meals = meals;
                return this.$http.get(BACK_URL + '/api/canteens/' + c.id.toString() + '/categories');
              })
              .then(response => response.json())
              .then(categories => {
                c.categories = categories;
              });
          }))
          .then(_ => this.canteens = canteens);
        });
    },
    createModal (canteen, meal) {
      if (!this.showModal) {
        this.modalCanteen = canteen;
        this.modalMealCopy = Object.assign({}, meal);
        this.modalMeal = meal;
        this.showModal = true;
      }
    },
    closeModal () {
      this.showModal = false;
    },
    confirmModal () {
      this.modalMealCopy.mealCategoryId = this.modalMealCopy.mealCategory.id;
      let sendObject = Object.assign({}, this.modalMealCopy);
      sendObject.mealCategory = null;
      if (this.modalMealCopy.hasOwnProperty('id')) {
        this.$http.put(BACK_URL + '/api/meals', sendObject)
          .then(response => {
            if (response.status == 200)
              Object.assign(this.modalMeal, this.modalMealCopy);
            else
              alert('Ошибка изменения.');
          })
          .catch(e => alert('Ошибка изменения.'));
      } else {
        this.$http.post(BACK_URL + '/api/meals', sendObject)
          .then(response => {
            return response.json();
          })
          .then(meal => this.modalCanteen.meals.push(meal))
          .catch(e => alert('Ошибка добавления.'));
      }
      this.closeModal();
    },
    editMeal (canteen, meal) {
      this.createModal(canteen, meal);
    },
    deleteMeal (canteen_index, meal_index) {
      if (!this.showModal) {
        let meal = this.canteens[canteen_index].meals[meal_index];
        this.$http.delete(BACK_URL + '/api/meals/' + meal.id.toString())
          .then(response => {
            if (response.status == 200) {
              this.canteens[canteen_index].meals.splice(meal_index, 1);
            }
          });
      }
    },
    createMeal (canteen) {
      this.createModal(canteen, {
        name: '',
        mealCategory: null,
        price: 0,
        weight: 0,
        calorie: 0
      });
    }
  },
  computed: {
    ...mapGetters(['isAuthenticated'])
  }
}
</script>

<style lang="scss">
  .content {
    padding: 5px 20px;
    display: block;
  }

  .form-control {
    margin: 10px auto;
  }

  .button-submit, .button-cancel, input[type="text"], input[type="password"], input[type="number"], select {
    box-sizing: content-box;
    padding: 4px 5px;
    border: 1px solid #666;
    border-radius: 3px;
    line-height: 21px;
    outline: none;
  }

  .button-form, input[type="text"], input[type="password"], input[type="number"], select {
    width: 300px;
  }

  button {
    cursor: pointer;
  }
  
  .button-submit {
    border-color: #003eff;
    background-color: #003eff;
    color: #fff;
  }

  .button-cancel {
    border-color: #ff0000;
    background-color: #ff0000;
    color: #fff;
  }

  .modal {
    position: absolute;
    background-color: #fff;
    width: 350px;
    text-align: center;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    box-shadow: 2px 2px 5px 1px #666;
    border-radius: 3px;
    padding: 10px;
    box-sizing: border-box;
  }

  .modal-close {
    position: absolute;
    top: 5px;
    right: 5px;
    cursor: pointer;
  }
</style>