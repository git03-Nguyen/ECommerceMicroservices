import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import { FieldValues } from "react-hook-form";
import { toast } from "react-toastify";
import agent from "../../app/api/agent";
import { User } from "../../app/models/user";
import { router } from "../../app/router/routes";
import { setBasket } from "../basket/basketSlice";
import { json } from "stream/consumers";
import { fetchProductsAsync } from "../catalog/catalogSlice";

interface AccountState {
  user: User | null;
}

const initialState: AccountState = {
  user: null,
};

export const logInUser = createAsyncThunk<User, FieldValues>(
  "account/logInUser",
  async (data, thunkAPI) => {
    try {
      const { basket, ...user } = await agent.Account.login(data);
      console.log({ basket, ...user });
      if (basket) thunkAPI.dispatch(setBasket(basket));
      localStorage.setItem("user", JSON.stringify(user));
      if (user) {
        thunkAPI.dispatch(setUser(user));
        thunkAPI.dispatch(fetchCurrentUser());
        thunkAPI.dispatch(fetchProductsAsync() as any);
      }
      return user;
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

export const fetchCurrentUser = createAsyncThunk<User>(
  "account/fetchCurrentUser",
  async (_, thunkAPI) => {
    const userInStorage = JSON.parse(localStorage.getItem("user")!);
    thunkAPI.dispatch(setUser(userInStorage));
    try {
      let response = null;
      let basket = null;
      let user = null;
      if (userInStorage.role != "Customer") {
        response = await agent.Admin.currentUser();
        basket = { basketItems: [] };
        user = { ...response.payload, token: userInStorage.token };
      } else {
        response = await agent.Account.currentUser();
        basket = response.basket.basket;
        user = { ...response.user.payload, token: userInStorage.token };
      }
      if (basket) thunkAPI.dispatch(setBasket(basket));
      localStorage.setItem("user", JSON.stringify(user));
      localStorage.setItem("basket", JSON.stringify(basket ?? []));
      return { ...user, basket };
    } catch (error) {
      return thunkAPI.rejectWithValue(error);
    }
  },
  {
    condition: () => {
      //only when we do not have "user" in local storage
      if (!localStorage.getItem("user")) return false;
    },
  }
);

export const accountSlice = createSlice({
  name: "account",
  initialState,
  reducers: {
    signOut: (state) => {
      state.user = null;
      localStorage.removeItem("user");
      router.navigate("/login");
    },
    setUser: (state, action) => {
      // let claims = JSON.parse(atob(action.payload.token.split(".")[1]));
      // let roles =
      //   claims["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      state.user = {
        ...action.payload,
        // roles: Array.isArray(roles) ? roles : [roles], //if it not an array, put it in an array
      };
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchCurrentUser.rejected, (state) => {
      state.user = null;
      localStorage.removeItem("user");
      toast.error("Session expired - please login again");
      router.navigate("/login");
    });
    builder.addMatcher(
      isAnyOf(logInUser.fulfilled),
      (state, action) => {
        // let claims = JSON.parse(atob(action.payload.token.split(".")[1]));
        // let role =
        //   claims[
        //   "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        //   ];
        state.user = {
          ...action.payload
        };
        router.navigate("/");
      }
    );
    builder.addMatcher(
      isAnyOf(fetchCurrentUser.fulfilled),
      (state, action) => {
        // let claims = JSON.parse(atob(action.payload.token.split(".")[1]));
        // let role =
        //   claims[
        //   "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        //   ];
        state.user = {
          ...action.payload
        };
      }
    );
    builder.addMatcher(isAnyOf(logInUser.rejected), (state, action) => {
      throw action.payload;
    });
  },
});

export const { signOut, setUser } = accountSlice.actions;
