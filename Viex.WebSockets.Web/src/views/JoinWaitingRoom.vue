<template>
  <div>

    <BasicHeader title="Join a Room"/>

    <div class="container mt-5">
      
      <div class="mb-4">
        <p class="mb-2">Username</p>
        <b-form-input placeholder="Enter your username" v-model="payload.username"></b-form-input>
      </div>
      
      <div class="mb-5">
        <p class="mb-2">Room Password</p>
        <b-form-input placeholder="Enter the room password" v-model="payload.roomPassword"></b-form-input>
      </div>

      <b-button block variant="primary" @click="onJoinWaitingRoomClicked">
        Join Room!
      </b-button>

    </div>
    
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { JoinWaitingRoomPayload } from "@/models/payloads/JoinWaitingRoomPayload";
import api from "@/api";
import storage from "@/storage";

@Component
export default class CreateWaitingRoomComponent extends Vue {

  payload = new JoinWaitingRoomPayload();

  constructor() {
    super();
  }

  async onJoinWaitingRoomClicked() {
    const joinedWaitingRoom = await api.waitingRooms.join(this.payload);
    console.log("joinedWaitingRoom is", joinedWaitingRoom);

    const joinedUser = joinedWaitingRoom.guests.find(user => user.username == this.payload.username);
    storage.session.setUser(joinedUser);
    storage.session.setWaitingRoomPassword(joinedWaitingRoom.password);
    
    this.$router.push("/waitingRoom");
  }
}
</script>
