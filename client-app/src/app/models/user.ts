export interface User {
    name: string;
    token: string;
}

export interface UserFormValues {
    email: string;
    password: string;
    username?: string;
    name?: string;
}

export interface UserConnectValues {
    gameId: string;
    name: string;
}