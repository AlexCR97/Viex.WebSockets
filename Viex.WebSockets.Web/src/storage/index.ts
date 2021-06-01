import { User } from "@/models/User";

enum SessionStorageItem {
    user = 'viex.user',
    waitingRoomPassword = 'viex.waitingRoomPassword',
}


const getSessionStorageItem = (item: SessionStorageItem) => {
    return sessionStorage.getItem(item);
}

const getSessionStorageObject = (item: SessionStorageItem) => {
    const json = sessionStorage.getItem(item);
    return JSON.parse(json);
}

const removeSessionStorageItem = (item: SessionStorageItem) => {
    sessionStorage.removeItem(item);
}

const setSessionStorageItem = (item: SessionStorageItem, value: any) => {
    sessionStorage.setItem(item, value);
}

const setSessionStorageItemObject = (item: SessionStorageItem, value: any) => {
    const json = JSON.stringify(value);
    sessionStorage.setItem(item, json);
}

export default {
    session: {
        getUser() {
            return getSessionStorageObject(SessionStorageItem.user) as User;
        },

        getWaitingRoomPassword() {
            return getSessionStorageItem(SessionStorageItem.waitingRoomPassword) as string;
        },

        removeUser() {
            removeSessionStorageItem(SessionStorageItem.user);
        },

        removeWaitingRoomPassword() {
            removeSessionStorageItem(SessionStorageItem.waitingRoomPassword);
        },

        setUser(user: User) {
            setSessionStorageItemObject(SessionStorageItem.user, user);
        },

        setWaitingRoomPassword(password: string) {
            setSessionStorageItem(SessionStorageItem.waitingRoomPassword, password);
        },
    },
};
