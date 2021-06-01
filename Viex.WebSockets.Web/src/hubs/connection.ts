import { HubConnectionBuilder } from "@microsoft/signalr";

const connection = new HubConnectionBuilder()
    .withUrl("https://localhost:5001/viexHub")
    .build();

export default connection;
