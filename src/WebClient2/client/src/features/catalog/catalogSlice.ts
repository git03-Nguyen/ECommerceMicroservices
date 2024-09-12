import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice,
  isAnyOf,
} from "@reduxjs/toolkit";
import { Product, ProductParams } from "../../app/models/product";
import agent from "../../app/api/agent";
import { RootState } from "../../app/store/configureStore";
import { MetaData } from "../../app/models/pagination";
import { number } from "yup";
import { act } from "react-dom/test-utils";

interface CatalogState {
  productsLoaded: boolean;
  filtersLoaded: boolean;
  status: string;
  categories: { categoryId: number; name: string; description: string; imageUrl: string }[];
  productParams: ProductParams;
  metaData: MetaData | null;
}

const productsAdapter = createEntityAdapter<Product>();

export const fetchProductAsync = createAsyncThunk<Product, number>(
  "catalog/fetchProductAsync",
  async (productId, thunkAPI) => {
    try {
      const { payload } = await agent.Catalog.details(productId);
      return payload;
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

// TODO: more params about prices, ...
function getAxiosParams(productParams: ProductParams) {
  const params = new URLSearchParams();
  params.append("pageNumber", productParams.pageNumber.toString());
  params.append("pageSize", productParams.pageSize.toString());
  params.append("orderBy", productParams.orderBy);
  if (productParams.searchTerm)
    params.append("searchTerm", productParams.searchTerm);
  if (productParams.categoryIds.length > 0)
    params.append("categoryIds", productParams.categoryIds.join(","));
  return params;
}

export const fetchProductsAsync = createAsyncThunk<
  Product[],
  void,
  { state: RootState }
>("catalog/fetchProductsAsync", async (_, thunkAPI) => {
  const params = getAxiosParams(thunkAPI.getState().catalog.productParams);
  try {
    const response = await agent.Catalog.list(params);
    console.log(response);
    const metaData = { pageNumber: response.pageNumber, pageSize: response.pageSize, totalPage: response.totalPage };
    const products = response.payload;
    thunkAPI.dispatch(setMetaData(metaData));
    return products;
  } catch (error: any) {
    thunkAPI.rejectWithValue({ error: error.data });
  }
});

// TODO: Fetch category filters
export const fetchFiltersAsync = createAsyncThunk(
  "catalog/FetchFiltersAsync",
  async (_, thunkAPI) => {
    try {
      const { payload } = await agent.Catalog.fetchFilters();
      return payload;
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

function initProductPrams() {
  return {
    pageNumber: 1,
    pageSize: 6,
    orderBy: "ProductId",
    categoryIds: [],
  };
}

export const catalogSlice = createSlice({
  name: "catalog",
  initialState: productsAdapter.getInitialState<CatalogState>({
    //more initial states other than "products"
    productsLoaded: false,
    filtersLoaded: false,
    status: "idle",
    categories: [],
    productParams: initProductPrams(),
    metaData: null,
  }),
  reducers: {
    setMetaData: (state, action) => {
      state.metaData = action.payload;
    },
    setPageNumber: (state, action) => {
      state.productsLoaded = false; // trigger fetchProductsAsync())in CatalogPage via useProduct()
      state.productParams = {
        ...state.productParams,
        ...action.payload,
      };
    },
    setProductsParams: (state, action) => {
      state.productsLoaded = false; // trigger fetchProductsAsync())in CatalogPage via useProduct()
      state.productParams = {
        ...state.productParams,
        ...action.payload,
        pageNumber: 1,
      };
      const categoryNames = action.payload.categoryIds;
      if (categoryNames) {
        const categoryIds = state.categories
          .filter((x) => categoryNames.includes(x.name))
          .map((x) => x.categoryId);
        state.productParams.categoryIds = categoryIds;
      }
    },
    resetProductsParams: (state) => {
      state.productParams = initProductPrams();
    },
    setProduct: (state, action) => {
      productsAdapter.upsertOne(state, action.payload);
      state.productsLoaded = false;
    },
    removeProduct: (state, action) => {
      productsAdapter.removeOne(state, action.payload);
      state.productsLoaded = false;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchProductAsync.pending, (state, action) => {
      state.status = "pendingFetchProduct";
    });
    builder.addCase(fetchProductAsync.fulfilled, (state, action) => {
      productsAdapter.upsertOne(state, action.payload);
      state.status = "idle";
    });

    builder.addCase(fetchProductsAsync.pending, (state, action) => {
      state.status = "pendingFetchProducts";
    });
    builder.addCase(fetchProductsAsync.fulfilled, (state, action) => {
      productsAdapter.setAll(state, action.payload);
      state.status = "idle";
      state.productsLoaded = true;
    });

    builder.addCase(fetchFiltersAsync.pending, (state) => {
      state.status = "pendingFetchFilters";
    });
    builder.addCase(fetchFiltersAsync.fulfilled, (state, action) => {
      state.categories = action.payload;
      state.filtersLoaded = true;
    });

    builder.addMatcher(
      isAnyOf(
        fetchProductAsync.rejected,
        fetchProductsAsync.rejected,
        fetchFiltersAsync.rejected
      ),
      (state, action) => {
        console.log(action);
        state.status = "idle";
      }
    );
  },
});

export const productSelectors = productsAdapter.getSelectors(
  (state: RootState) => state.catalog
);

export const {
  setProductsParams,
  setMetaData,
  setPageNumber,
  setProduct,
  removeProduct,
  resetProductsParams,
} = catalogSlice.actions;