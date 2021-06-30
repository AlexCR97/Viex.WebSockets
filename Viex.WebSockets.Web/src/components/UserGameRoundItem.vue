<template>

    <div v-if="showNone" class="app-center-both p-2">
        <p class="m-0" style="color: gray">none</p>
    </div>

    <!-- user -->
    <div v-else class="p-2">
        <p class="text-center m-0">{{item.username}}</p>
        <p class="text-center font-weight-bold m-0">{{item.turn}}</p>
    </div><!-- user -->

</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class UserGameRoundItemComponent extends Vue {

    @Prop() item: UserGameRoundItem;

    get showNone() {
        return this.item.turn <= 0;
    }
    
}

export interface UserGameRoundItem {
    username: string;
    turn: number;
}
</script>
