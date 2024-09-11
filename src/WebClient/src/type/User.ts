export interface User {
    id: string
    userName: string
    email: string
    password: string
    role: "admin" | "customer"
    avatar: string
    token: string
}

export interface UserState {
    users: User[],
    currentUser?: User | null,
    loading: boolean,
    error: string | null
}

export interface UserCredentials {
    userName: string
    password: string
}

// export interface UserUpdate {
//     id: number
//     update: Omit<User, "id">
// }

export interface UserUpdate {
    id: string
    userName: string
    token: string
}

export interface NewUser {
    userName: string
    email: string
    password: string
    // token: string
}

export interface DeleteUser {
    id: string
    token: string
}