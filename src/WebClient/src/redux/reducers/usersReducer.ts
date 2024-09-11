import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

import { DeleteUser, NewUser, User, UserCredentials, UserState, UserUpdate } from "../../type/User";
import { BASE_URL } from "../../type/Shared";
import { RootState } from "../rootReducer";

const initialState: UserState = {
    users: [],
    loading: false,
    error: null,
    currentUser: null
}

export const fetchAllUsers = createAsyncThunk(
    "fetchAllUsers",
    async () => {
        try {
            const response = await axios.get<User[]>(`${BASE_URL}/users`)
            return response.data
        }
        catch (e) {
            const error = e as AxiosError
            if (error.response) {
                return JSON.stringify(error.response.data)
            }
            return error.message
        }
    }
)

export const getUserById = async (id: string) => {
    try {
        const response = await axios.get<User>(`${BASE_URL}/users/${id}`)
        return response.data
    }
    catch (e) {
        const error = e as AxiosError
        if (error.response) {
            return JSON.stringify(error.response.data)
        }
        return error.message
    }
}

export const authenticate = createAsyncThunk(
    "authenticate",
    async (access_token: string) => {
        try {
            const authentication = await axios.get<{ payload: User }>(`${BASE_URL}/AuthService/User/GetOwnProfile`, {
                headers: {
                    "Authorization": `Bearer ${access_token}`
                }
            });
            return authentication.data.payload;
        } catch (err) {
            const error = err as AxiosError;
            if (error.response) {
                return JSON.stringify(error.response.data);
            }
            return error.message;
        }
    }
)

export const login = createAsyncThunk(
    "login",
    async ({ userName, password }: UserCredentials, { dispatch }) => {
        try {
            const result = await axios.post(`${BASE_URL}/AuthService/User/LogIn`, { userName, password });
            const token = result.data.accessToken;
            // const expiresIn = result.data.expiresIn;
            const authentication = await dispatch(authenticate(token));
            console.log(`authentication: ${JSON.stringify(authentication)}`);
            return { user: authentication.payload, token };
        }
        catch (err) {
            const error = err as AxiosError;
            if (error.response) {
                return JSON.stringify(error.response.data)
            }
            return error.message;
        }
    }
)

export const registerUser = createAsyncThunk(
    "signup",
    async ({ userName, email, password }: NewUser) => {
        try {
            const response = await axios.post(`${BASE_URL}/users`, { userName, email, password }
            );
            return response.data;
        }
        catch (e) {
            const error = e as AxiosError
            if (error.response) {
                return JSON.stringify(error.response.data)
            }
            return error.message;
        }
    }
)

export const CreateAdmin = createAsyncThunk(
    "createAdmin",
    async ({ userName, email, password }: NewUser, { getState }) => {
        try {
            const state = getState() as RootState;
            const token = state.users.currentUser?.token;
            const response = await axios.post(`${BASE_URL}/users/admin`, { userName, email, password },
                {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                });
            return response.data;
        }
        catch (e) {
            const error = e as AxiosError
            if (error.response) {
                return JSON.stringify(error.response.data)
            }
            return error.message
        }
    }
)

export const updateUser = createAsyncThunk(
    "updateUser",
    async ({ id, userName }: UserUpdate, { getState }) => {
        try {
            const state = getState() as RootState;
            const token = state.users.currentUser?.token
            const response = await axios.put(`${BASE_URL}/users/${id}`, { userName },
                {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                });
            return response.data;
        } catch (err) {
            const error = err as AxiosError;
            if (error.response) {
                return JSON.stringify(error.response.data);
            }
            return error.message;
        }
    }
);

export const deleteUser = createAsyncThunk(
    "deleteUser",
    async ({ id, token }: DeleteUser) => {
        try {
            const response = await axios.delete(`${BASE_URL}/users/${id}`,
                {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                });
            return response.data;
        } catch (err) {
            const error = err as AxiosError
            if (error.response) {
                return JSON.stringify(error.response.data);
            }
            return error.message;
        }
    }
)

const usersSlice = createSlice({
    name: "users",
    initialState,
    reducers: {
        cleanUpUsersReducer: (state) => {
            return initialState
        },
        logout: (state) => {
            localStorage.removeItem("token")
            state.currentUser = null
            state.error = null
        }
    },
    extraReducers: (build) => {
        build
            .addCase(fetchAllUsers.fulfilled, (state, action) => {
                if (typeof action.payload === "string") {
                    state.error = action.payload
                }
                else {
                    state.users = action.payload
                }
                state.loading = false
            })
            .addCase(fetchAllUsers.pending, (state) => {
                state.loading = true
                state.error = ""
            })
            .addCase(fetchAllUsers.rejected, (state, action) => {
                state.error = action.error.message as string
                state.loading = false
            })
            .addCase(registerUser.pending, (state) => {
                state.loading = true
                state.error = ""
            })
            .addCase(registerUser.fulfilled, (state, action) => {
                state.loading = false
                if (typeof action.payload === "string") {
                    state.error = action.payload;
                }
                else {
                    state.users.push(action.payload)
                }
            })
            .addCase(registerUser.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            })
            .addCase(CreateAdmin.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(CreateAdmin.fulfilled, (state, action) => {
                if (typeof action.payload === "string") {
                    state.error = action.payload;
                }
                else {
                    state.users.push(action.payload)
                }
            })
            .addCase(login.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.currentUser = null;
            })
            .addCase(login.fulfilled, (state, action) => {
                state.loading = false;
                if (typeof action.payload === "string") {
                    state.error = action.payload;
                    state.currentUser = null;
                }
                else {
                    state.error = null;
                    state.currentUser = action.payload.user as User;
                    state.currentUser.token = action.payload.token;
                    localStorage.setItem('token', action.payload.token);
                }
            })
            .addCase(login.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message!
                state.currentUser = null;
            })
            .addCase(updateUser.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(updateUser.fulfilled, (state, action) => {
                state.loading = false;
                if (typeof action.payload === "string") {
                    state.error = action.payload;
                }
                else {
                    state.error = null;
                    state.currentUser = action.payload;
                }
            })
            .addCase(updateUser.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            })
            .addCase(deleteUser.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(deleteUser.fulfilled, (state, action) => {
                state.loading = false;
                state.users = state.users.filter(user => user.id !== action.payload)
            })
            .addCase(deleteUser.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message as string;
            })
    }
})

const usersReducer = usersSlice.reducer
export const { cleanUpUsersReducer, logout } = usersSlice.actions
export default usersReducer