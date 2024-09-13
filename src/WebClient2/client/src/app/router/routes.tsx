import { Navigate, createBrowserRouter } from "react-router-dom";
import HomePage from "../../features/home/HomePage";
import App from "../layout/App";
import CatalogPage from "../../features/catalog/CatalogPage";
import ProductDetails from "../../features/catalog/ProductDetails";
import TestPage from "../../features/test/TestPage";
import ContactPage from "../../features/contact/ContactPage";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import BasketPage from "../../features/basket/BasketPage";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import RequireAuth from "./RequireAuth";
import Orders from "../../features/orders/Orders";
import CheckoutWrapper from "../../features/checkout/CheckoutWrapper";
import Inventory from "../../features/seller/product/Inventory";
import CategoryInventory from "../../features/admin/category/CategoryInventory";
import Profile from "../../features/account/Profile";
import UserPage from "../../features/admin/user/UserPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        // Authenticated routes
        element: <RequireAuth roles={["Customer", "Seller", "Admin"]} />,
        children: [
          { path: "profile", element: <Profile /> },
        ]
      },

      {
        // Customer routes
        element: <RequireAuth roles={["Customer"]} />,
        children: [
          { path: "checkout", element: <CheckoutWrapper /> },
          { path: "orders", element: <Orders /> },
          { path: "basket", element: <BasketPage /> },
        ],
      },

      {
        // Admin routes
        element: <RequireAuth roles={["Admin"]} />,
        children: [
          { path: "inventory-management", element: <Inventory /> }, //adjust later
          { path: "category-management", element: <CategoryInventory /> },
          { path: "user-management", element: <UserPage /> }
        ],
      },

      // Seller routes
      {
        element: <RequireAuth roles={["Seller"]} />,
        children: [
          { path: "inventory", element: <Inventory /> }
        ],
      },

      // Public routes
      { path: "", element: <HomePage /> },
      { path: "catalog", element: <CatalogPage /> },
      { path: "catalog/:id", element: <ProductDetails /> },

      // Authentication routes
      { path: "login", element: <Login /> },
      { path: "register", element: <Register /> },

      // Error routes
      { path: "test", element: <TestPage /> },
      { path: "server-error", element: <ServerError /> },
      { path: "not-found", element: <NotFound /> },
      { path: "*", element: <Navigate replace to="/not-found" /> },

      // Other static pages
      // { path: "contact", element: <ContactPage /> },

    ],
  },
]);
