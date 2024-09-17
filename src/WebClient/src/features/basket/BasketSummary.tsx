import {
  TableContainer,
  Paper,
  Table,
  TableBody,
  TableRow,
  TableCell,
} from "@mui/material";

import { currencyFormat, vndCurrencyFormat } from "../../app/util/util";
import { useAppSelector } from "../../app/store/configureStore";

interface Props {
  subtotal?: number;
  isBasket?: boolean;
}

export default function BasketSummary({ subtotal, isBasket = true }: Props) {
  const { basket } = useAppSelector((state) => state.basket);

  if (subtotal === undefined)
    subtotal =
      basket?.basketItems.reduce(
        (sum, item) => sum + item.quantity * item.unitPrice,
        0
      ) ?? 0;

  const deliveryFee = 0; // free delivery for now

  return (
    <>
      <TableContainer component={Paper} variant={"outlined"}>
        <Table>
          <TableBody>
            <TableRow>
              <TableCell colSpan={2}>Subtotal</TableCell>
              <TableCell align="right">{vndCurrencyFormat(subtotal)}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell colSpan={2}>Delivery fee</TableCell>
              <TableCell align="right">{vndCurrencyFormat(deliveryFee)}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell colSpan={2}>Total</TableCell>
              <TableCell align="right">
                {vndCurrencyFormat(subtotal + deliveryFee)}
              </TableCell>
            </TableRow>

            {isBasket && (<TableRow>
              <TableCell>
                <span style={{ fontStyle: "italic" }}>
                  *Double check your order before proceeding
                </span>
              </TableCell>
            </TableRow>)}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
}
