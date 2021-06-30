<template>
    <div>

        <BasicHeader title="Game Room" :confirmBack="true"/>
        
        <div class="container my-4">

            <!-- starting round -->
            <div v-if="isStartingRound">
                <Loader/>
            </div><!-- starting round -->

            <!-- round started! -->
            <div v-else>
                
                <!-- remaining seconds and round letter -->
                <div class="row mb-4">

                    <!-- remaining seconds -->
                    <div class="col">
                        <p class="font-weight-normal text-center m-0">Remaining Time</p>
                        <h1 class="text-center m-0">30</h1>
                    </div><!-- remaining seconds -->

                    <!-- round letter -->
                    <div class="col">
                        <p class="font-weight-normal text-center m-0">Round Letter</p>
                        <h1 class="font-weight-normal text-center m-0">A</h1>
                    </div><!-- round letter -->

                </div><!-- remaining seconds and round letter -->

                <!-- finished turns -->
                <div class="mb-4">

                    <p class="text-center mb-2">Finished Turns</p>

                    <b-card class="shadow-sm" no-body>
                        <div class="row">
                            <div v-for="(item, index) of finishedTurns" :key="index" class="col app-center-x">
                                <UserGameRoundItem :item="item"></UserGameRoundItem>
                            </div>
                        </div>
                    </b-card>

                </div><!-- finished turns -->
                
                <div v-for="category of gameConceptCategories" :key="category" class="mb-3">
                    <p class="mb-1">{{category}}</p>
                    <b-form-input class="shadow-sm"></b-form-input>
                </div>

                <b-button block class="mt-4" variant="primary" @click="onConfirmClicked">
                    Confirm
                </b-button>

            </div><!-- round started! -->

        </div>

        <LoadingModal ref="loadingModal"/>
        <AlertModal ref="alertModal"/>

    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import LoadingModalComponent from '@/components/modals/LoadingModal.vue';
import { UserGameRoundItem } from '@/components/UserGameRoundItem.vue';
import storage from '@/storage';
import { GameRoom } from '@/models/GameRoom';
import api from '@/api';
import hubs from '@/hubs';
import AlertModalComponent from '@/components/modals/AlertModal.vue';
import timers from "@/utils/timers";

@Component
export default class GameRoomComponent extends Vue {
    
    room = new GameRoom();
    isStartingRound = true;
    finishedTurns: UserGameRoundItem[] = [];

    alertModal = this.$refs.alertModal as AlertModalComponent;
    loadingModal = this.$refs.loadingModal as LoadingModalComponent;

    constructor() {
        super()
        this.init();
    }

    get gameConceptCategories() {
        return this.room.currentRoundGameConcepts.map(a => a.description);
    }

    onConfirmClicked() {
        this.loadingModal.open({
            title: "Your turn finished!",
            message: "Please wait while others finish their turns",
        });
    }

    private init() {
        console.log("this.$refs:", this.$refs);
        this.isStartingRound = true;
        //this.loadingModal.open({ title: "Starting Round" });

        this.setSocketListeners();

        const currentUser = storage.session.getUser();
        if (currentUser.isHost)
            this.startRound();
    }

    private setSocketListeners() {
        hubs.gameRooms.onRoundStarted(async (room) => {
            this.isStartingRound = false;
            //this.loadingModal.close();
            this.room = room;
            console.log("this.room:", this.room);

            //this.alertModal.open({ title: "Round Started!", message: "Go!" });
            await timers.wait(1000);
            //this.alertModal.close();

            // TODO Start countdown (host only)
            console.log("Start countdown!");
        });
    }

    private async startRound() {
        const gameRoomPassword = storage.session.getWaitingRoomPassword();
        await api.gameRooms.startRound(gameRoomPassword);
        hubs.gameRooms.startRound(gameRoomPassword);
    }
}
</script>
