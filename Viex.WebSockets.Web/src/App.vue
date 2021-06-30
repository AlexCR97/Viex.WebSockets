<template>
  <div id="app">
    <router-view/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import api from "@/api";
import storage from "@/storage";

@Component
export default class AppComponent extends Vue {

  constructor() {
    super();
    this.init();
  }

  async init() {
    const gameConcepts = await api.gameConcepts.getCategories();
    storage.local.setGameConceptCategories(gameConcepts);
  }
}
</script>

<style>
.app-border-red {
  border: 2px solid red;
}

.app-border-blue {
  border: 2px solid blue;
}

.app-border-green {
  border: 2px solid green;
}

.app-w-100 {
  width: 100%;
}

.app-h-100 {
  height: 100%;
}

.app-h-100vh {
  height: 100vh;
}

.app-center-both {
  display: flex;
  justify-content: center;
  align-items: center;
}

.app-center-x {
  display: flex;
  justify-content: center;
}

.app-center-y {
  display: flex;
  align-items: center;
}
</style>
