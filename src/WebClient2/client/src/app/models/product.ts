export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
  stock?: number;
  categoryId?: number;
  categoryName?: string;
  sellerAccountId?: string;
  sellerName?: string;
  publicId: string;
}

export interface ProductParams {
  orderBy: string;
  searchTerm?: string;
  categoryIds: number[];
  pageNumber: number;
  pageSize: number;
}
