import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import axios, { AxiosError } from "axios";

import { BASE_URL, FetchAllParams } from "../../type/Shared";
import { Category, CategoryState, NewCategory } from "../../type/Category";
import { RootState } from "../rootReducer";
import { Product } from "../../type/Product";

const initialState: CategoryState = {
  categories: [],
  loading: false,
  error: null,
  categoryProducts: [],
  singleCategory: null
};

export const fetchAllCategories = createAsyncThunk(
  'fetchAllCategories',
  async ({
    searchKeyword = null,
    sortBy = 'UpdatedAt',
    sortDescending = false,
    pageNumber = 1,
    pageSize = 10,
  }: FetchAllParams) => {
    try {
      const response = await axios.get<{ payload: Category[] }>(`${BASE_URL}/CatalogService/Category/Get`
        //   , {
        //   params: {
        //     SearchKeyword: searchKeyword,
        //     SortBy: sortBy,
        //     SortDescending: sortDescending,
        //     PageNumber: pageNumber,
        //     PageSize: pageSize,
        //   },
        // }
      );
      console.log(response.data.payload);
      return response.data.payload;
    } catch (e) {
      const error = e as AxiosError
      if (error.response) {
        return (error.response.data as any)?.message || 'An unknown error occurred';
      }
      return error.message;
    }
  }
);

export const fetchSingleCategory = createAsyncThunk(
  "category",
  async (categoryId: number | undefined) => {
    try {
      const respone = await axios.get<Category>(`${BASE_URL}/categories/${categoryId}`);
      return respone.data;
    } catch (err) {
      const error = err as AxiosError
      if (error.response) {
        return (error.response.data as any)?.message || 'An unknown error occurred';
      }
      return error.message;
    }
  }
)

export const fetchProductsByCategory = createAsyncThunk(
  "category/products",
  async (categoryId: number | undefined) => {
    try {
      const respone = await axios.get<Product[]>(`${BASE_URL}/CatalogService/Product/Get?CategoryId=${categoryId}`);
      return respone.data;
    } catch (err) {
      const error = err as AxiosError
      if (error.response) {
        return (error.response.data as any)?.message || 'An unknown error occurred';
      }
      return error.message;
    }
  }
)

export const createCategory = createAsyncThunk(
  "createCategory",
  async (category: NewCategory, { getState }) => {
    try {
      const state = getState() as RootState;
      const token = state.users.currentUser?.token;
      const response = await axios.post(`${BASE_URL}/categories`, category,
        {
          headers: {
            "Authorization": `Bearer ${token}`
          }
        })
      return response.data;
    } catch (err) {
      const error = err as AxiosError
      if (error.response) {
        return (error.response.data as any)?.message || 'An unknown error occurred';
      }
      return error.message;
    }
  }
)

export const updateCategory = createAsyncThunk(
  "updateCategory",
  async (category: Category, { getState }) => {
    try {
      const state = getState() as RootState;
      const token = state.users.currentUser?.token

      const response = await axios.put(`${BASE_URL}/categories/${category.categoryId}`, category,
        {
          headers: {
            "Authorization": `Bearer ${token}`
          }
        })
      return response.data;
    } catch (err) {
      const error = err as AxiosError
      if (error.response) {
        return (error.response.data as any)?.message || 'An unknown error occurred';
      }
      return error.message;
    }
  }
)

export const deleteCartegory = createAsyncThunk(
  "deleteCartegory",
  async (cartegoryId: number, { getState }) => {
    try {
      const state = getState() as RootState;
      const token = state.users.currentUser?.token
      if (token === null) {
        throw new Error('Authorization token not found.');
      }
      const response = await axios.delete(`${BASE_URL}/categories/${cartegoryId}`, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      })
      return response.data
    }
    catch (e) {
      const error = e as AxiosError
      if (error.response) {
        return (error.response.data as any)?.message || 'An unknown error occurred';
      }
      return error.message;
    }
  }
)

const categoriesSlice = createSlice({
  name: 'category',
  initialState,
  reducers: {
    cleanUpCategoryReducer: (state) => {
      return initialState;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchAllCategories.pending, (state) => {
        state.loading = true;
        state.error = '';
      })
      .addCase(fetchAllCategories.fulfilled, (state, action) => {
        state.loading = false;
        state.singleCategory = null;
        if (typeof action.payload === "string") {
          state.error = action.payload

        }
        else {
          state.categories = action.payload
        }
        // state.categories = action.payload as Category[];
      })
      .addCase(fetchAllCategories.rejected, (state, action) => {
        state.loading = false;
        state.singleCategory = null;
        state.error = action.error.message as string;
      })
      .addCase(fetchSingleCategory.pending, (state) => {
        state.loading = false;
        state.error = null;
      })
      .addCase(fetchSingleCategory.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload
        }
        else {
          state.singleCategory = action.payload
        }
      })
      .addCase(fetchSingleCategory.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error as string;
      })
      .addCase(fetchProductsByCategory.pending, (state) => {
        state.loading = false;
        state.error = null;
      })
      .addCase(fetchProductsByCategory.fulfilled, (state, action) => {
        state.loading = false
        if (typeof action.payload === "string") {
          state.error = action.payload
        }
        else {
          state.categoryProducts = action.payload
        }
      })
      .addCase(fetchProductsByCategory.rejected, (state, action) => {
        state.loading = false
        state.error = action.error.message as string
      })
      .addCase(createCategory.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(createCategory.fulfilled, (state, action) => {
        state.loading = false;
        if (typeof action.payload === "string") {
          state.error = action.payload;
        }
        else {
          state.loading = false;
          state.categories.push(action.payload);
        }
      })
      .addCase(createCategory.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message as string;
      })
      .addCase(updateCategory.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(updateCategory.fulfilled, (state, action) => {
        state.loading = false;
        const updatedCategoryIndex = state.categories.findIndex(
          category => category.categoryId === action.payload.id
        );
        if (updatedCategoryIndex !== -1) {
          state.categories[updatedCategoryIndex] = action.payload;
        }
      })
      .addCase(updateCategory.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message as string;
      })
      .addCase(deleteCartegory.pending, (state) => {
        state.loading = true;
      })
      .addCase(deleteCartegory.fulfilled, (state, action) => {
        state.loading = false
        if (action.payload instanceof AxiosError) {
          state.error = action.payload.message
        }
        else {
          const categoryId = action.meta.arg
          state.categories = state.categories.filter((category) => category.categoryId !== categoryId)
        }
      })
      .addCase(deleteCartegory.rejected, (state, action) => {
        state.loading = false
        state.error = action.payload as string
      })
  },
});

const categoriesReducer = categoriesSlice.reducer;
const { cleanUpCategoryReducer } = categoriesSlice.actions;

export default categoriesReducer;