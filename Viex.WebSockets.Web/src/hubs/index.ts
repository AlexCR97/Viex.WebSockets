import WaitingRoomHub from "./WaitingRoomHub";

export default {
    waitingRooms: WaitingRoomHub,
}

export type HubListener<T> = (value: T) => void;
