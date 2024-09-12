// import { Elements } from "@stripe/react-stripe-js";
// import { loadStripe } from "@stripe/stripe-js";
import CheckoutPage from "./CheckoutPage";
import { useState, useEffect } from "react";
import agent from "../../app/api/agent";
import { useAppDispatch } from "../../app/store/configureStore";
import { setBasket } from "../basket/basketSlice";
import LoadingComponent from "../../app/layout/LoadingComponent";

import { useAppSelector } from "../../app/store/configureStore";

// const stripePromise = loadStripe(process.env.REACT_APP_STRIPE_PUBLIC_KEY!);

export default function CheckoutWrapper() {
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);


  useEffect(() => {
    ///// get payment intent once in Checkout Page => TODO
    // agent.Payments.createPaymentIntent()

    // agent.Basket.initCheckout()
    //   .then((response) => dispatch(setBasket(response)))
    //   .catch((error) => console.log(error))
    //   .finally(() => setLoading(false));
    setLoading(false);
  }, [dispatch]);

  if (loading) return <LoadingComponent message="Loading checkout" />;

  return (
    // <Elements stripe={stripePromise}>
    // </Elements>
    <CheckoutPage />
  );
}
