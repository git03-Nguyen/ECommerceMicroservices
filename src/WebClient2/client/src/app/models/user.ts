import { Basket } from "./basket";

export interface User {
  userId: string;
  userName: string;
  email: string;
  token: string;
  basket?: Basket;
  role: "Admin" | "Customer" | "Seller";
  fullName?: string;
  address?: string;
  phoneNumber?: string;
  paymentDetails?: string;
}
