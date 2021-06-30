<template>
    <div>
        
        <BasicHeader title="Waiting Room" :confirmBack="true"/>

        <div class="container my-5">

            <!-- loader room -->
            <div v-if="loadingRoom">
                <Loader/>
            </div>

            <!-- room loaded -->
            <div v-else>

                <!-- title -->
                <div class="mb-4">
                    <h5 class="text-center font-weight-normal">Welcome to room <b>{{room.password}}</b></h5>
                    <h1 class="text-center">{{remainingSeconds}}</h1>
                </div>

                <!-- host user -->
                <b-card class="shadow mb-3">
                    <p class="m-0">
                        <b>Host</b> 
                        <span :class="{ 'font-weight-bold': isThisMyUserName(hostUserName), 'text-success': isThisMyUserName(hostUserName) }">
                            {{hostUserName}}
                        </span>
                    </p>
                </b-card><!-- host user -->

                <!-- guests -->
                <b-card v-for="username of guestUserNames" :key="username" class="shadow">
                    <p class="m-0">
                        <b>Guest</b>
                        <span :class="{ 'font-weight-bold': isThisMyUserName(username), 'text-success': isThisMyUserName(username) }">
                            {{username}}
                        </span>
                    </p>
                </b-card>
            </div><!-- room loaded -->

        </div>

        <LoadingModal ref="loadingModal"/>

    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import hubs from "@/hubs";
import storage from "@/storage";
import api from "@/api";
import { WaitingRoom } from '@/models/WaitingRoom';
import LoadingModalComponent from '@/components/modals/LoadingModal.vue';

@Component
export default class WaitingRoomComponent extends Vue {

    room = new WaitingRoom();
    loadingRoom = true;
    
    remainingSeconds = 15;

    constructor() {
        super();
        this.init();
    }

    get currentUser() {
        return storage.session.getUser();
    }

    get guestUserNames() {
        return this.room.guests.map(g => g.username);
    }

    get hostUserName() {
        return this.currentUser.isHost
            ? this.currentUser.username
            : this.room.host.username;
    }

    get loadingModal() {
        return this.$refs.loadingModal as LoadingModalComponent;
    }

    isThisMyUserName(username: string) {
        return storage.session.getUser().username == username;
    }

    private async init() {
        const waitingRoomPassword = storage.session.getWaitingRoomPassword();

        this.room = await api.waitingRooms.getByPassword(waitingRoomPassword);
        this.loadingRoom = false;

        hubs.waitingRooms.onGuestJoinedWaitingRoom(username => {
            console.log("guest joined room!", username);
        });

        hubs.waitingRooms.onRemainingSecondsElapsed(remainingSeconds => {
            this.remainingSeconds = remainingSeconds;
        });

        hubs.waitingRooms.onTimeUp(async () => {
            this.loadingModal.open({
                title: "Starting Game",
                message: "Please wait. Game will start shortly",
            });

            if (this.currentUser.isHost)
                await this.startGame(waitingRoomPassword);
        });

        hubs.gameRooms.onGameStarted(() => {
            this.loadingModal.close();
            this.$router.push("/gameRoom");
        });

        hubs.waitingRooms.join({
            username: this.currentUser.username,
            roomPassword: waitingRoomPassword,
        });

        if (this.currentUser.isHost) {
            hubs.waitingRooms.startCountDown(waitingRoomPassword);
        }
    }

    private async startGame(waitingRoomPassword: string) {
        await api.gameRooms.startGame(waitingRoomPassword);
        hubs.gameRooms.startGame(waitingRoomPassword);
    }
}
</script>
