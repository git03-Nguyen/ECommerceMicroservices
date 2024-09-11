import { Product } from "./Product"

export interface Category {
    categoryId: number
    name: string
    description: string
    imageUrl: string
}

export interface CategoryState {
    categories: Category[]
    loading: boolean
    error: string | null
    categoryProducts?: Product[]
    singleCategory?: Category | null
}

export interface NewCategory {
    name: string
    description: string
    imageUrl: string
}

export interface EditCategoryModalProps {
    open: boolean;
    category: Category;
    onClose: () => void;
    onSubmit: (updatedCategory: Category) => void;
}
