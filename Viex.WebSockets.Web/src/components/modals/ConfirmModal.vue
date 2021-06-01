<template>
    <div>
        <b-modal v-model="opened"
            :title="options.title"
            :cancel-title="options.cancelButtonText"
            :ok-title="options.confirmButtonText"
            centered
            hide-header-close
            no-close-on-backdrop
            no-close-on-esc
            @hidden="onCancelClicked"
            @ok="onConfirmClicked">
            {{options.message}}
        </b-modal>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class ConfirmModalComponent extends Vue {

    options: ConfirmModalOptions;
    opened = false;

    private readonly defaultOptions: ConfirmModalOptions = {
        title: "Confirm",
        message: "Are you sure you want to perform this action?",
        cancelButtonText: "No",
        confirmButtonText: "Yes",
    };

    private resolve: () => void;
    private reject: (reason?: any) => void;

    constructor() {
        super();
        this.initOptions(this.defaultOptions);
    }

    open(options?: ConfirmModalOptions) {
        this.initOptions(options);
        this.opened = true;

        return new Promise<void>((resolve, reject) => {
            this.resolve = resolve;
            this.reject = reject;
        });
    }

    onCancelClicked() {
        this.reject();
        this.opened = false;
    }

    onConfirmClicked() {
        this.resolve();
        this.opened = false;
    }

    private initOptions(options?: ConfirmModalOptions) {
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

            cancelButtonText: options.cancelButtonText
                ? options.cancelButtonText
                : this.defaultOptions.cancelButtonText,

            confirmButtonText: options.confirmButtonText
                ? options.confirmButtonText
                : this.defaultOptions.confirmButtonText,
        };
    }
}

export interface ConfirmModalOptions {
    title?: string;
    message?: string;
    cancelButtonText?: string;
    confirmButtonText?: string;
}
</script>
