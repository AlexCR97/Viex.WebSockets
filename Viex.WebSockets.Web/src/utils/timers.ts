export default {
    wait(miliseconds: number) {
        return new Promise<void>(resolve => {
            setTimeout(() => resolve(), miliseconds);
        });
    }
}
