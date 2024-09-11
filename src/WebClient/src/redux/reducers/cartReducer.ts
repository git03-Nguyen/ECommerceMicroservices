import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { CartItem, CartState } from "../../type/Cart";

const initialState: CartState = {
  items: []
}

const cartSlice = createSlice({
  name: "shoppingCart",
  initialState,
  reducers: {
    addToCart: (state, action: PayloadAction<CartItem>) => {
      state.items.push(action.payload)
    },
    removeFromCart: (state, action: PayloadAction<{ productId: number }>) => {
      state.items = state.items.filter(
        (item) => item.product.productId !== action.payload.productId
      );
    },
    updateCartItemQuantity: (state, action: PayloadAction<{ productId: number, quantity: number }>) => {
      const index = state.items.findIndex((item) => item.product.productId === action.payload.productId);
      if (index !== -1) {
        const updatedItem = { ...state.items[index] };
        updatedItem.quantity = action.payload.quantity;
        const updatedItems = [...state.items];
        updatedItems[index] = updatedItem;
        state.items = updatedItems;
      }
    }
  }
})


export const { addToCart, removeFromCart, updateCartItemQuantity } = cartSlice.actions;

export default cartSlice.reducer;