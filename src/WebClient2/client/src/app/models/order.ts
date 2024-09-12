export interface ShippingAddress {
  fullName: string;
  address: string;
  phoneNumber: string;
}

export interface OrderItem {
  productId: number;
  productName: string;
  imageUrl: string;
  unitPrice: number;
  quantity: number;
}

export interface Order {
  id: number;
  buyerEmail: string;
  orderDate: string;
  shippingAddress: ShippingAddress;
  deliveryFee: number;
  orderItems: OrderItem[];
  subtotal: number;
  orderStatus: string;
  total: number;
}
