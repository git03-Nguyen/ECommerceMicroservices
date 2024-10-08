import {
  Divider,
  Grid,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import NotFound from "../../app/errors/NotFound";
import { LoadingButton } from "@mui/lab";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import {
  addBasketItemAsync,
  removeBasketItemAsync,
} from "../basket/basketSlice";
import { fetchProductAsync, productSelectors } from "./catalogSlice";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { vndCurrencyFormat } from "../../app/util/util";

const catalogUrl = process.env.REACT_APP_CATALOG_URL!;

export default function ProductDetails() {
  const dispatch = useAppDispatch();
  const { basket, status: basketStatus } = useAppSelector(
    (state) => state.basket
  );
  const { status: productStatus } = useAppSelector((state) => state.catalog);
  const { id } = useParams<{ id: string }>();
  const product = useAppSelector((state) =>
    productSelectors.selectById(state, id!)
  );
  const { user } = useAppSelector((state) => state.account);

  const [quantity, setQuantity] = useState(0);

  const item = basket?.basketItems.find((item) => item.productId === product?.id);

  useEffect(() => {
    if (item) setQuantity(item.quantity);
    if (!product) dispatch(fetchProductAsync(parseInt(id!)));
  }, [id, item, dispatch, product]);

  function handleInputChange(e: any) {
    if (e.target.value >= 0) setQuantity(parseInt(e.target.value));
  }

  function handleUpdateCart() {
    if (!item || quantity > item?.quantity) {
      const quantityDiff = item ? quantity - item.quantity : quantity;
      dispatch(
        addBasketItemAsync({ productId: product!.id, quantity: quantityDiff })
      );
    } else {
      const quantityDiff = item.quantity - quantity;
      dispatch(
        removeBasketItemAsync({
          productId: product!.id,
          quantity: quantityDiff,
        })
      );
    }
  }

  if (productStatus === "pendingFetchProduct")
    return <LoadingComponent message="Loading product..." />;
  if (!product) return <NotFound />;

  return (
    <Grid container spacing={6}>
      <Grid item xs={6}>
        <img
          src={product.imageUrl?.startsWith("http") ? product.imageUrl : `${catalogUrl}${product.imageUrl}`}
          alt={product.name}
          style={{ width: "100%" }}
        />
      </Grid>
      <Grid item xs={6}>
        <Typography variant="h3">{product.name}</Typography>
        <Divider sx={{ mb: 2 }} />
        <Typography variant="h3">
          {vndCurrencyFormat(product.price)}
        </Typography>
        <TableContainer>
          <Table>
            <TableBody>
              <TableRow>
                <TableCell>Name</TableCell>
                <TableCell>{product.name}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Description</TableCell>
                <TableCell>{product.description}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Category</TableCell>
                <TableCell>{product.categoryName}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>Seller</TableCell>
                <TableCell>{product.sellerName} - {product.sellerAccountId}</TableCell>
              </TableRow>
              <TableRow>
                <TableCell>In Stock</TableCell>
                <TableCell>{product.stock}</TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
        {user && user.role == "Customer" && (
          <Grid container spacing="2">
            <Grid item xs={6}>
              <TextField
                onChange={handleInputChange}
                variant="outlined"
                type="number"
                label="Quantity in Cart"
                fullWidth
                value={quantity}
              />
            </Grid>
            <Grid item xs={6}>
              <LoadingButton
                disabled={
                  item?.quantity === quantity || (!item && quantity === 0)
                }
                loading={basketStatus.includes("pending")}
                onClick={handleUpdateCart}
                sx={{ height: "55px" }}
                color={"primary"}
                size={"large"}
                variant={"contained"}
                fullWidth
              >
                {item ? "Update Quantity" : "Add to Cart"}
              </LoadingButton>
            </Grid>
          </Grid>
        )}
      </Grid>
    </Grid>
  );
}
