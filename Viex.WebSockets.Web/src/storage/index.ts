import { User } from "@/models/User";

enum LocalStorageItem {
    gameConceptCategories = 'viex.gameConceptCategories',
}

enum SessionStorageItem {
    user = 'viex.user',
    waitingRoomPassword = 'viex.waitingRoomPassword',
}

const getLocalStorageItemObject = (item: LocalStorageItem) => {
    const json = localStorage.getItem(item);
    return JSON.parse(json);
}

const removeLocalStorageItem = (item: LocalStorageItem) => {
    localStorage.removeItem(item);
}

const setLocalStorageItemObject = (item: LocalStorageItem, value: any) => {
    const json = JSON.stringify(value);
    localStorage.setItem(item, json);
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
    local: {
        getGameConceptCategories() {
            return getLocalStorageItemObject(LocalStorageItem.gameConceptCategories) as string[];
        },

        setGameConceptCategories(categories: string[]) {
            setLocalStorageItemObject(LocalStorageItem.gameConceptCategories, categories);
        },
    },

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
