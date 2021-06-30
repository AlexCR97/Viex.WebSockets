import { User } from "./User";

export class WaitingRoom {
    waitingRoomId: number;
    password: string;
    remainingSeconds: number;
    
    hostId: number;
    host: User;

    guests: User[];
}
