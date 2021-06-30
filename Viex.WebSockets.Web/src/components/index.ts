import Vue from 'vue';
import BasicHeader from "@/components/BasicHeader.vue";
import ConfirmModal from "@/components/modals/ConfirmModal.vue";
import Loader from "@/components/Loader.vue";
import LoadingModal from "@/components/modals/LoadingModal.vue";
import UserGameRoundItem from "@/components/UserGameRoundItem.vue";
import AlertModal from "@/components/modals/AlertModal.vue";

Vue.component("BasicHeader", BasicHeader);
Vue.component("ConfirmModal", ConfirmModal);
Vue.component("Loader", Loader);
Vue.component("LoadingModal", LoadingModal);
Vue.component("UserGameRoundItem", UserGameRoundItem);
Vue.component("AlertModal", AlertModal);
