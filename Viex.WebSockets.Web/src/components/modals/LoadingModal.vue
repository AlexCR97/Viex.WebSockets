<template>
    <div>
        <b-modal v-model="opened"
            :title="options.title"
            centered
            hide-header-close
            hide-footer
            no-close-on-backdrop
            no-close-on-esc>
            <div class="py-3">
                <p class="text-center">
                    {{options.message}}
                </p>

                <div class="app-center-x">
                    <b-spinner type="grow" style="width: 60px; height: 60px"></b-spinner>
                </div>
            </div>
        </b-modal>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class LoadingModalComponent extends Vue {
    
    options: LoadingModalOptions;
    opened = false;

    private readonly defaultOptions = <LoadingModalOptions> {
        title: "Loading",
        message: "Please wait",
    };

    constructor() {
        super();
        this.initOptions(this.defaultOptions);
    }

    close() {
        this.opened = false;
    }

    open(options?: LoadingModalOptions) {
        this.initOptions(options);
        this.opened = true;
    }

    private initOptions(options?: LoadingModalOptions) {
        if (!options) {
            this.options = this.defaultOptions;
            return;
        }

        this.options = {
            title: options.title
                ? options.title
                : this.defaultOptions.title,
            message: options.message
                ? options.message
                : this.defaultOptions.message,
        };
    }
}

export interface LoadingModalOptions {
    title?: string;
    message?: string;
}
</script>
