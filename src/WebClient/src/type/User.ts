export interface User {
    id: string
    userName: string
    email: string
    password: string
    role: "Admin" | "Customer" | "Seller"
    fullName: string
    phoneNumber: string
    address: string
    paymentDetails: "Debit Card" | "Paypal" | "COD"
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
    email: string
    fullName: string
    phoneNumber: string
    address: string
    paymentDetails: string
}

export interface NewUser {
    userName: string
    email: string
    password: string
    fullName: string
    phoneNumber: string
    address: string
    role: "Admin" | "Customer" | "Seller"
    // token: string
}

export interface DeleteUser {
    id: string
    token: string
}