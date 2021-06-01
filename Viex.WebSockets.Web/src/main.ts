import 'mutationobserver-shim'
import Vue from 'vue';
import './plugins/bootstrap-vue'
import App from './App.vue';
//import "@/plugins/signalr.plugin";
import router from './router'
import "@/components";
import connection from "@/hubs/connection";
import "@/api/config";

Vue.config.productionTip = true;

connection.start().then(() => {
    console.log("Connected! Starting app...");
    
    new Vue({
        router,
        render: h => h(App)
    }).$mount('#app');
});
