import ProductList from "./ProductList";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { fetchProductsAsync, setPageNumber, setProductsParams } from "./catalogSlice";
import { Grid, Paper } from "@mui/material";
import AppPagination from "../../app/components/AppPagination";
import CheckboxButtons from "../../app/components/CheckboxButtons";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import ProductSearch from "./ProductSearch";
import useProducts from "../../app/hooks/useProducts";
import { string } from "yup";
import { useEffect } from "react";

const sortOptions = [
  { value: "name", label: "Alphabetical" },
  { value: "priceDesc", label: "Price - High to low" },
  { value: "price", label: "Price - Low to high" },
];

export default function CatalogPage() {
  const { products, categories, filtersLoaded, metaData } = useProducts();
  const { productParams } = useAppSelector((state) => state.catalog);
  const dispatch = useAppDispatch();

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
