<template>
  <div class="container app-h-100vh app-center-both">
    <div>

      <h2 class="text-center mb-4">Basta Online</h2>

      <b-card class="shadow" style="min-width: 320px">
        <div>
          <b-button class="mb-3" block variant="primary" to="/createWaitingRoom">Create Room</b-button>
          <b-button block variant="danger" to="/joinWaitingRoom">Join Room</b-button>
        </div>
      </b-card>

    </div>
  </div>
</template>

<script>
export default {

}
</script>
