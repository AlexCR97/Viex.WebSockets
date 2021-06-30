import { GameConcept } from "./GameConcept";
import { User } from "./User";

export class GameRoom {
    gameRoomId: number;
    password: string;
    totalRounds: number;
    currentRound: number;
    currentRoundLetter: string;
    currentRoundRemainingSeconds: number;

    hostId: number;
    host: User;

    players: User[];

    currentRoundGameConcepts: GameConcept[];
};
