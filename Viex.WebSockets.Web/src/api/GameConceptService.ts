import axios from "axios";
import { IRestApiService } from "./IRestApiService";

export class GameConceptService implements IRestApiService {

    endpoint: string = "gameConcept";

    async getCategories() {
        const url = `${this.endpoint}/categories`;
        const response = await axios.get<string[]>(url);
        return response.data;
    }

}
