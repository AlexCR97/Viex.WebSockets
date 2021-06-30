import { GameRoom } from "@/models/GameRoom";
import { HubListener } from ".";
import connection from "./connection";

export default {
    startGame(gameRoomPassword: string) {
        return connection.invoke("StartGame", gameRoomPassword);
    },

    startRound(gameRoomPassword: string) {
        return connection.invoke("StartRound", gameRoomPassword);
    },

    onGameStarted(listener: HubListener<void>) {
        connection.on("GameStarted", listener);
    },

    onRoundStarted(listener: HubListener<GameRoom>) {
        connection.on("RoundStarted", listener);
    }
};
