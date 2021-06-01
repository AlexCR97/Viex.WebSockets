<template>
  <div>

    <BasicHeader title="Create a Room"/>

    <div class="container mt-5">
      
      <div class="mb-4">
        <p class="mb-2">Username</p>
        <b-form-input placeholder="Enter your username" v-model="payload.username"></b-form-input>
      </div>
      
      <div class="mb-5">
        <p class="mb-2">Room Password</p>
        <b-form-input placeholder="Enter the room password" v-model="payload.roomPassword"></b-form-input>
      </div>

      <b-button block variant="primary" @click="onCreateWaitingRoomClicked">
        Create Room!
      </b-button>

    </div>
    
  </div>
</template>

<script lang="ts">
import { CreateWaitingRoomPayload } from '@/models/payloads/CreateWaitingRoomPayload';
import { Component, Vue } from 'vue-property-decorator';
import api from "@/api";
import storage from "@/storage";

@Component
export default class CreateWaitingRoomComponent extends Vue {

  payload = new CreateWaitingRoomPayload();

  constructor() {
    super();
  }

  async onCreateWaitingRoomClicked() {
    const createdWaitingRoom = await api.waitingRooms.create(this.payload);
    console.log("createdWaitingRoom is", createdWaitingRoom);

    storage.session.setUser(createdWaitingRoom.host);
    storage.session.setWaitingRoomPassword(createdWaitingRoom.password);
    
    this.$router.push("/waitingRoom");
  }
}
</script>
