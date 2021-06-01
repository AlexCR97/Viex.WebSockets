<template>
  <div>

    <!-- header -->
    <div class="row py-2 shadow">

      <div class="col app-center-both">
        <b-button variant="link" @click="onBackClicked">Back</b-button>
      </div>

      <div class="col-6 app-center-both">
        <h5 class="mb-1">{{title}}</h5>
      </div>

      <div class="col"></div>

    </div><!-- header -->

    <ConfirmModal ref="confirmModal"/>

  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import ConfirmModalComponent from './modals/ConfirmModal.vue';

@Component
export default class BasicHeaderComponent extends Vue {

  @Prop() title: string;
  @Prop({ default: "Back" }) backText: string;
  @Prop({ default: false }) confirmBack: boolean;

  get confirmModal() {
    return this.$refs.confirmModal as ConfirmModalComponent;
  }

  onBackClicked() {
    if (!this.confirmBack) {
      this.$router.back();
      return;
    }

    this.confirmModal.open({
      title: "Go Back",
      message: "Are you sure you want to go back?",
    })
    .then(() => this.$router.back())
    .catch(() => {});
  }
}
</script>
