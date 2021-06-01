import { IRestApiService } from "./IRestApiService";
import axios from 'axios';
import { CreateWaitingRoomPayload } from "@/models/payloads/CreateWaitingRoomPayload";
import { WaitingRoom } from "@/models/WaitingRoom";
import { JoinWaitingRoomPayload } from "@/models/payloads/JoinWaitingRoomPayload";

export class WaitingRoomService implements IRestApiService {
    
    endpoint: string = 'waitingRoom';

    async create(payload: CreateWaitingRoomPayload) {
        const url = `${this.endpoint}/create`;
        const response = await axios.post<WaitingRoom>(url, payload);
        return response.data;
    }

    async join(payload: JoinWaitingRoomPayload) {
        const url = `${this.endpoint}/join`;
        const response = await axios.post<WaitingRoom>(url, payload);
        return response.data;
    }

    async getByPassword(password: string) {
        const url = `${this.endpoint}/password/${password}`;
        const response = await axios.get<WaitingRoom>(url);
        return response.data;
    }
}
