<template>
    <div>
        <b-skeleton animation="fade" class="mb-3"></b-skeleton>
        <b-skeleton animation="fade" class="mb-3"></b-skeleton>
        <b-skeleton animation="fade" class="mb-3"></b-skeleton>
        <b-skeleton animation="fade" class="mb-3"></b-skeleton>
        <b-skeleton animation="fade" class="mb-3"></b-skeleton>
        <b-skeleton animation="fade" class="mb-3"></b-skeleton>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class LoaderComponent extends Vue {
    
}
</script>
