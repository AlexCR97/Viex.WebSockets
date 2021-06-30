import { GameRoom } from "@/models/GameRoom";
import axios from "axios";
import { IRestApiService } from "./IRestApiService";

export class GameRoomService implements IRestApiService {
    
    endpoint: string = "gameRoom";

    async startGame(roomPassword: string) {
        const url = `${this.endpoint}/startGame/${roomPassword}`;
        const response = await axios.post<GameRoom>(url, {});
        return response.data;
    }

    async startRound(roomPassword: string) {
        const url = `${this.endpoint}/startRound/${roomPassword}`;
        const response = await axios.put<GameRoom>(url, {});
        return response.data;
    }
};
