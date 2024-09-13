import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/routes";
import { PaginatedResponse } from "../models/pagination";
import { store } from "../store/configureStore";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;
axios.defaults.withCredentials = true; //allow cross-site cookies

axios.interceptors.request.use((config) => {
  //add JWT token to header
  const token = store.getState().account.user?.token;
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

const sleep = () => new Promise((resolve) => setTimeout(resolve, 500));
axios.interceptors.response.use(
  async (response) => {
    // if (process.env.NODE_ENV === "development") await sleep(); //used to test loading

    const pagination = response.headers["pagination"];
    if (pagination) {
      response.data = new PaginatedResponse(
        response.data,
        JSON.parse(pagination)
      );
    }
    return response;
  },
  (error: AxiosError) => {
    const { data, status } = error.response as AxiosResponse;

    switch (status) {
      case 400:
        if (data.errors) {
          //validation errors
          const modelStateErrors = Object.values(data.errors);
          throw modelStateErrors.flat();
        }
        toast.error(data.title);
        break;
      case 401:
        toast.error(data.title || "Unauthorised");
        break;
      case 404:
        // toast.error(data.title);
        break;
      case 500:
        router.navigate("/server-error", { state: { error: data } });
        break;
      default:
        break;
    }
    return Promise.reject(error.response);
  }
);

const responseBody = (response: AxiosResponse) => response.data;
const requests = {
  get: (url: string, params?: URLSearchParams) =>
    axios.get(url, { params }).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
  postForm: (url: string, data: FormData) =>
    axios
      .post(url, data, {
        headers: { "Content-type": "multipart/form-data" },
      })
      .then(responseBody),
  putForm: (url: string, data: FormData) =>
    axios
      .put(url, data, {
        headers: { "Content-type": "multipart/form-data" },
      })
      .then(responseBody),
};

const Account = {
  login: (values: any) => requests.post("AuthService/User/Login", values),
  register: (values: any) => requests.post("AuthService/User/SignUp", values),
  currentUser: () => requests.get("Aggregates/UserBasket"),
  fetchAddress: (userId: string) => requests.get(`UserService/User/GetById${userId}`),
};

function createFormData(item: any) {
  let formData = new FormData();
  for (const key in item) {
    formData.append(key, item[key]);
  }
  return formData;
}

const Admin = {
  // Categories
  createCategory: (category: any) =>
    requests.post("CatalogService/Category/Add", category),
  updateCategory: (category: any) =>
    requests.put("CatalogService/Category/Update", category),
  deleteCategory: (id: number) =>
    requests.delete(`CatalogService/Category/Delete/${id}`),

  // Products
  createProduct: (product: any) =>
    requests.post("CatalogService/Product/Add", product),
  createProductForm: (product: any) =>
    requests.postForm("CatalogService/Product/Add", createFormData(product)),
  updateProduct: (product: any) =>
    requests.put("CatalogService/Product/Update", product),
  updateProductForm: (product: any) =>
    requests.putForm("CatalogService/Product/Update", createFormData(product)),
  deleteProduct: (id: number) => requests.delete(`CatalogService/Product/Delete/${id}`),

  // Orders
  listOrders: () => requests.get("OrderService/Order/GetAll"),
  updateOrder: (order: any) => requests.put("OrderService/Order/Update", order),
  deleteOrder: (id: number) => requests.delete(`OrderService/Order/Delete/${id}`),

  // Users
  listUsers: () => requests.get("UserService/User/Get"),
  createUser: (user: any) => requests.post("AuthService/User/SignUp", user),
  updateUser: (user: any) => requests.put("UserService/User/Update", user),
  deleteUser: (id: string) => requests.delete(`AuthService/User/Delete/${id}`),
};

const Basket = {
  get: () => requests.get("BasketService/Basket/Get").catch(),
  addItem: (productId: number, quantity = 1) =>
    requests.post(`BasketService/Basket/Increase`, { productId, quantity }),
  removeItem: (productId: number, quantity = 1) =>
    requests.post(`BasketService/Basket/Decrease`, { productId, quantity }),
  checkout: (data: any) => requests.post("BasketService/Basket/Checkout", data),
};

const Catalog = {
  list: (params: URLSearchParams) => requests.get("CatalogService/Product/Get", params),
  details: (id: number) => requests.get(`CatalogService/Product/GetById/${id}`),
  fetchFilters: () => requests.get(`CatalogService/Category/Get`),
};

const Orders = {
  list: () => requests.get("OrderService/Order/GetOwnOrders"),
  fetch: (id: number) => requests.get(`orders/${id}`),
  create: (values: any) => requests.post("orders", values),
};

const Payments = {
  createPaymentIntent: () => requests.post("payments", {}),
};

const TestErrors = {
  get400Error: () => requests.get("Test/bad-request"),
  get401Error: () => requests.get("Test/unauthorised"),
  get404Error: () => requests.get("Test/not-found"),
  get500Error: () => requests.get("Test/server-error"),
  getValidationError: () => requests.get("Test/validation-error"),
};

const agent = {
  Account,
  Admin,
  Basket,
  Catalog,
  Orders,
  TestErrors,
  Payments,
};

export default agent;
