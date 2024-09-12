// export interface ShippingAddress {
//   fullName: string;
//   address: string;
//   phoneNumber: string;
// }

export interface OrderItem {
  productId: number;
  sellerAccountId: string;
  productName: string;
  imageUrl: string;
  unitPrice: number;
  quantity: number;
}

export interface Order {
  orderId: number;
  buyerId: string;
  recipientName: string;
  shippingAddress: string;
  recipientPhone: string;
  orderItems: OrderItem[];
  status: string;
  totalPrice: number;
  createdDate: string;
  updatedDate: string;

  // buyerEmail: string;
  // orderDate: string;
  // deliveryFee: number;
  // subtotal: number;
  // orderStatus: string;
  // total: number;
}
