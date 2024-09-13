import { useEffect } from "react";
import {
  fetchFiltersAsync
} from "../../features/catalog/catalogSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function useCategories() {
  const { categories, filtersLoaded } =
    useAppSelector((state) => state.catalog);

  const dispatch = useAppDispatch();

  useEffect(() => {
    if (!filtersLoaded) dispatch(fetchFiltersAsync());
  }, [filtersLoaded, dispatch]);

  return { categories, filtersLoaded };
}
