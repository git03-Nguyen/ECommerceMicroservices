import { Category } from "./Category"

export interface Product {
    productId: number
    name: string
    description: string
    price: number
    imageUrl: string
    category: Category
    categoryId: number
    stock: number
    sellerAccountId: string
    sellerName: string
}

export interface ProductState {
    products: Product[]
    loading: boolean
    error: string | null
    singleProduct: Product | null
}

export interface CreateProduct {
    name: string
    description: string
    imageUrl: string
    price: number
    stock: number
    categoryId: number
}

export interface UpdateProduct {
    productId: number
    name: string
    description: string
    imageUrl: string
    price: number
    stock: number
    categoryId: number
}

export interface ProductUpdate {
    productId: number
    update: Partial<Omit<Product, "productId">> & { productId?: never }
}

export interface ProductProps {
    product: Product
}

export interface FileUploadResponse {
    originalname: string;
    filename: string;
    location: string;
}

export interface EditProductModalProps {
    open: boolean;
    product: UpdateProduct;
    onClose: () => void;
    onSubmit: (updatedProduct: UpdateProduct) => void;
}

export interface fetchSingleProductProps {
    loading: boolean
    error: string | null
    product: Product | null
}

export interface FavoriteButtonProps {
    favProduct: Product;
}