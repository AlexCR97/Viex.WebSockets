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
            </div>

        </b-modal>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class AlertModalComponent extends Vue {
    
    options: AlertModalOptions;
    opened = false;

    private readonly defaultOptions = <AlertModalOptions> {
        title: "Title",
        message: "Message",
    };

    constructor() {
        super();
        this.initOptions(this.defaultOptions);
    }

    close() {
        this.opened = false;
    }

    open(options?: AlertModalOptions) {
        this.initOptions(options);
        this.opened = true;
    }

    private initOptions(options?: AlertModalOptions) {
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

export interface AlertModalOptions {
    title?: string;
    message?: string;
}
</script>
