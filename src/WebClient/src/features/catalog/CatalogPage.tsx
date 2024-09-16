import ProductList from "./ProductList";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { fetchProductsAsync, setPageNumber, setProductsParams } from "./catalogSlice";
import { debounce, Grid, Paper, TextField } from "@mui/material";
import AppPagination from "../../app/components/AppPagination";
import CheckboxButtons from "../../app/components/CheckboxButtons";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import ProductSearch from "./ProductSearch";
import useProducts from "../../app/hooks/useProducts";
import { string } from "yup";
import { useEffect, useState } from "react";

const sortOptions = [
  { value: "Name&SortOrder=asc", label: "Name - Ascending" },
  { value: "Name&SortOrder=desc", label: "Name - Descending" },
  { value: "Price&SortOrder=asc", label: "Price - Low to high" },
  { value: "Price&SortOrder=desc", label: "Price - High to low" },
];

export default function CatalogPage() {
  const { products, categories, filtersLoaded, metaData } = useProducts();
  const { productParams } = useAppSelector((state) => state.catalog);
  const [minPrice, setMinPrice] = useState(productParams.minPrice);
  const [maxPrice, setMaxPrice] = useState(productParams.maxPrice);
  const dispatch = useAppDispatch();

  const debouncedMin = debounce((event: any) => {
    dispatch(setProductsParams({ minPrice: Number(event.target.value) }));
  }, 1000);

  const debouncedMax = debounce((event: any) => {
    dispatch(setProductsParams({ maxPrice: Number(event.target.value) }));
  }, 1000);

  if (!filtersLoaded)
    return <LoadingComponent message="Loading products ... " />;

  return (
    <Grid container columnSpacing={4}>
      <Grid item xs={3}>
        <Paper sx={{ mb: 2 }}>
          <ProductSearch />
        </Paper>
        <Paper sx={{ p: 2, mb: 2 }}>
          <RadioButtonGroup
            selectedValue={productParams.orderBy}
            options={sortOptions}
            onChange={(e) =>
              dispatch(setProductsParams({ orderBy: e.target.value }))
            }
          />
        </Paper>

        <Paper sx={{ p: 2, mb: 2 }}>
          <div style={{ display: "flex", gap: "8px" }}>
            <TextField
              variant="outlined"
              label="Min"
              type="number"
              size="small" // Add size="small" to make the text field smaller
              value={minPrice || undefined}
              onChange={(e) => {
                setMinPrice(Number(e.target.value));
                debouncedMin(e);
              }
              }
            />
            <div style={{ display: "flex", alignItems: "center" }}>
              <span>-</span>
            </div>
            <TextField
              variant="outlined"
              label="Max"
              type="number"
              size="small" // Add size="small" to make the text field smaller
              value={maxPrice || undefined}
              onChange={(e) => {
                setMaxPrice(Number(e.target.value));
                debouncedMax(e);
              }
              }
            />
          </div>
        </Paper>

        {categories.length > 0 &&
          (<Paper sx={{ p: 2 }}>
            <CheckboxButtons
              items={categories.map(x => x.name)}
              checked={productParams.categoryIds.map(String)}
              onChange={(checkedItems: string[]) =>
                dispatch(setProductsParams({ categoryNames: checkedItems }))
              }
            />
          </Paper>)}
      </Grid>
      <Grid item xs={9}>
        <ProductList products={products} />
      </Grid>
      <Grid item xs={3} />
      <Grid item xs={9} sx={{ my: 2 }}>
        {metaData && (
          <AppPagination
            metaData={metaData}
            onPageChange={(page: number) =>
              dispatch(setPageNumber({ pageNumber: page }))
            }
          />
        )}
      </Grid>
    </Grid>
  );
}
