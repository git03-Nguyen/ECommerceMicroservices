import {
  TableContainer,
  Paper,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Button,
} from "@mui/material";
import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { Order } from "../../app/models/order";
import { currencyFormat, vndCurrencyFormat } from "../../app/util/util";
import OrderDetailed from "./OrderDetailed";

export default function Orders() {
  const [orders, setOrders] = useState<Order[] | null>(null);
  const [loading, setLoading] = useState(true);
  const [selectedOrderNumber, setSelectedOrderNumber] = useState(0);

  useEffect(() => {
    agent.Orders.list()
      .then((response) => setOrders(response.payload))
      .catch((error) => console.log(error))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <LoadingComponent message="Loading orders..." />;

  if (selectedOrderNumber > 0)
    return (
      <OrderDetailed
        order={orders?.find((o) => o.orderId === selectedOrderNumber)!}
        setSelectedOrder={setSelectedOrderNumber}
      />
    );

  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>#</TableCell>
            <TableCell align="left">Ordered Date</TableCell>
            <TableCell align="left">Shipping Address</TableCell>
            <TableCell align="left">Phone Number</TableCell>
            <TableCell align="left">Recipient Name</TableCell>
            <TableCell align="center">Items</TableCell>
            <TableCell align="right">Total</TableCell>
            <TableCell align="right">Order Status</TableCell>
            <TableCell align="right"></TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {orders?.map((order) => (
            <TableRow
              key={order.orderId}
              sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {order.orderId}
              </TableCell>
              <TableCell align="left">
                {order.createdDate.split("T")[0]}
              </TableCell>
              <TableCell align="left">{order.shippingAddress.length > 30 ? order.shippingAddress.substring(0, 30) + "..." : order.shippingAddress}</TableCell>
              <TableCell align="left">{order.recipientPhone}</TableCell>
              <TableCell align="left">{order.recipientName}</TableCell>
              <TableCell align="center">{order.orderItems.reduce((acc, item) => acc + item.quantity, 0)}</TableCell>
              <TableCell align="right">{vndCurrencyFormat(order.totalPrice)}</TableCell>
              <TableCell align="right"
                style={{
                  color: order.status === "Pending" ? "orange" : order.status === "Delivered" ? "green" : "red", fontWeight: "bold"
                }}
              >
                {order.status}
              </TableCell>
              <TableCell align="right">
                <Button onClick={() => setSelectedOrderNumber(order.orderId)}>
                  View
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
