import { GameConceptService } from "./GameConceptService";
import { GameRoomService } from "./GameRoomService";
import { WaitingRoomService } from "./WaitingRoomService";

export default {
    gameConcepts: new GameConceptService(),
    gameRooms: new GameRoomService(),
    waitingRooms: new WaitingRoomService(),
};
