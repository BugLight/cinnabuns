<template>
  <div class="main">
    <div class="content" v-if="isAuthenticated">
      <h1>Панель администратора</h1>
      <button class="button-cancel" v-on:click="logout">Выйти</button>
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
      password: ''
    };
  },
  methods: {
    login () {
      this.$store.dispatch('auth', {
        name: this.name,
        password: this.password
      }).then(token => {
        alert('Вход выполнен успешно!');
      }).catch(e => {
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

  .button-submit, .button-cancel, input[type="text"], input[type="password"] {
    box-sizing: content-box;
    padding: 4px 5px;
    border: 1px solid #666;
    border-radius: 3px;
    line-height: 21px;
    outline: none;
  }

  .button-form, input[type="text"], input[type="password"] {
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
</style>