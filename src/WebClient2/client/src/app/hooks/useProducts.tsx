import { useEffect } from "react";
import {
  productSelectors,
  fetchProductsAsync,
  fetchFiltersAsync,
} from "../../features/catalog/catalogSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function useProducts() {
  const { productsLoaded, filtersLoaded, categories, metaData } =
    useAppSelector((state) => state.catalog);

  const dispatch = useAppDispatch();
  const products = useAppSelector(productSelectors.selectAll);

  useEffect(() => {
    if (!productsLoaded) dispatch(fetchProductsAsync());
  }, [productsLoaded, dispatch]);

  useEffect(() => {
    if (!filtersLoaded) dispatch(fetchFiltersAsync());
  }, [filtersLoaded, dispatch]);

  return { products, productsLoaded, filtersLoaded, categories, metaData };
}
