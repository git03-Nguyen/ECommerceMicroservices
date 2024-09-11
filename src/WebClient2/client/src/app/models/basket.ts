export interface Basket {
  basketId: number;
  accountId: string;
  basketItems: BasketItem[];
  paymentIntentId: string;
  clientSecret: string;
}

export interface BasketItem {
  basketItemId: number;
  sellerAccountId: string;
  productId: number;
  productName: string;
  imageUrl: string;
  unitPrice: number;
  categoryId: number;
  quantity: number;
}
