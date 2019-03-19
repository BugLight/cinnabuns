<template>
  <div class="main">
    <h1 class="title-main">{{canteen ? canteen.name : null}}</h1>
    <div class="container">
      <div class="filters">
        <h3 style="line-height: 0;">Фильтрация блюд</h3>
        <div class="item">
          Категории блюд
          <div class="category-selected" v-for="category in categories" :key="category.id">
            <input type="checkbox" :value="category.id" :id="`category-${category.id}`" @change="setCategoryFilter(category.id)"/>
            <label :for="`category-${category.id}`">{{category.name}}</label>
          </div>
        </div>
        <div class="item">
          Цена
          <div class="rotate-cont">
            <div class="rotate-count" id="rub-left"></div>
            <div class="rotate-count" id="rub-right"></div>
          </div>
          <div id="slider-range"></div>
        </div>
        <div class="item">
          Калорийность
          <div class="rotate-cont">
            <div class="rotate-count" id="cal-left"></div>
            <div class="rotate-count" id="cal-right"></div>
          </div>
          <div id="calory-range"></div>
        </div>
      </div>
      <div class="meals-container"></div>
      <div class="info"></div>
    </div>
  </div>
</template>
<script src="./scripts/canteen.js"></script>
<style lang="scss">
@import "./styles/canteen.scss";
</style>