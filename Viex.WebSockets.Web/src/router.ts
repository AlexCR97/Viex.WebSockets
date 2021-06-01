import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home.vue'
import CreateWaitingRoom from "@/views/CreateWaitingRoom.vue"
import JoinWaitingRoom from "@/views/JoinWaitingRoom.vue";
import WaitingRoom from "@/views/WaitingRoom.vue";

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      redirect: "/home",
    },
    {
      path: '/home',
      name: 'Home',
      component: Home,
    },
    {
      path: '/createWaitingRoom',
      name: 'CreateWaitingRoom',
      component: CreateWaitingRoom,
    },
    {
      path: '/joinWaitingRoom',
      name: 'JoinWaitingRoom',
      component: JoinWaitingRoom,
    },
    {
      path: '/waitingRoom',
      name: 'WaitingRoom',
      component: WaitingRoom,
    },
  ]
})
