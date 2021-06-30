import WaitingRoomHub from "./WaitingRoomHub";
import GameRoomHub from "./GameRoomHub";

export default {
    waitingRooms: WaitingRoomHub,
    gameRooms: GameRoomHub,
}

export type HubListener<T> = (value: T) => void;
