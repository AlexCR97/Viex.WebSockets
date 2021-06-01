import { JoinWaitingRoomPayload } from "@/models/payloads/JoinWaitingRoomPayload";
import { LeaveWaitingRoomPayload } from "@/models/payloads/LeaveWaitingRoomPayload";
import { HubListener } from ".";
import connection from "./connection";

export default {
    join(payload: JoinWaitingRoomPayload) {
        payload.roomPassword = `${payload.roomPassword}`;
        return connection.invoke("JoinWaitingRoom", payload);
    },

    leave(payload: LeaveWaitingRoomPayload) {
        payload.roomPassword = `${payload.roomPassword}`;
        return connection.invoke("LeaveWaitingRoom", payload);
    },

    startCountDown(waitingRoomPassword: string) {
        return connection.invoke("StartCountDown", waitingRoomPassword);
    },

    onGuestJoinedWaitingRoom(listener: HubListener<string>) {
        connection.on("GuestJoinedWaitingRoom", listener);
    },

    onGuestLeftWaitingRoom(listener: HubListener<string>) {
        connection.on("GuestLeftWaitingRoom", listener);
    },

    onRemainingSecondsElapsed(listener: HubListener<number>) {
        connection.on("WaitingRoomRemainingSecondsElapsed", listener);
    },

    onTimeUp(listener: HubListener<void>) {
        connection.on("WaitingRoomTimeUp", listener);
    },
};
