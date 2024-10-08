import {
  Box,
  Button,
  Paper,
  Step,
  StepLabel,
  Stepper,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import AddressForm from "./AddressForm";
import PaymentForm from "./PaymentForm";
import Review from "./Review";
import { FieldValues, FormProvider, useForm } from "react-hook-form";
import { LoadingButton } from "@mui/lab";
import { yupResolver } from "@hookform/resolvers/yup";
import { validationSchema } from "./checkoutValidation";
import agent from "../../app/api/agent";
import { clearBasket, fetchBasketItemAsync } from "../basket/basketSlice";
import { useDispatch } from "react-redux";
// import { StripeElementType } from "@stripe/stripe-js";
// import {
//   CardNumberElement,
//   useElements,
//   useStripe,
// } from "@stripe/react-stripe-js";
import { useAppSelector } from "../../app/store/configureStore";
import { toast } from "react-toastify";
import { fetchProductAsync, fetchProductsAsync, resetProductsLoaded } from "../catalog/catalogSlice";

// const steps = ["Shipping Information", "Review your order", "Payment details"];
const steps = ["Shipping Information", "Review your order"];
const catalogUrl = process.env.REACT_APP_CATALOG_URL!;

export default function CheckoutPage() {
  const dispatch = useDispatch();
  // const stripe = useStripe();
  // const elements = useElements();
  const [activeStep, setActiveStep] = useState(0);
  const [orderNumber, setOrderNumber] = useState(0);
  const [loading, setLoading] = useState(false);
  // const [cardState, setCardState] = useState<{
  //   elementError: { [key in StripeElementType]?: string };
  // }>({ elementError: {} });
  // const [cardComplete, setCardComplete] = useState<any>({
  //   cardNumber: false,
  //   cardExpiry: false,
  //   cardCvc: false,
  // });
  const [paymentMessage, setPaymentMessage] = useState("");
  const [paymentSucceeded, setPaymentSucceeded] = useState(false);
  const { basket } = useAppSelector((state) => state.basket);
  const { user } = useAppSelector((state) => state.account);

  const currentValidationSchema = validationSchema[activeStep];
  const methods = useForm({
    mode: "onBlur",
    resolver: yupResolver(currentValidationSchema),
  });

  useEffect(() => {
    // get current user from state

    // set default values for the form
    methods.reset({
      fullName: user?.fullName,
      shippingAddress: user?.address,
      phoneNumber: user?.phoneNumber,
      saveAddress: false,
    });



  }, [methods]);

  function onCardInputChange(e: any) {
    // setCardState({
    //   ...cardState,
    //   elementError: {
    //     ...cardState.elementError,
    //     [e.elementType]: e.error?.message,
    //   },
    // });
    // setCardComplete({ ...cardComplete, [e.elementType]: e.complete });
  }

  function getStepContent(step: number) {
    switch (step) {
      case 0:
        return <AddressForm />;
      case 1:
        return <Review />;
      // case 2:
      //   return (
      //     <PaymentForm
      //       cardState={cardState}
      //       onCardInputChange={onCardInputChange}
      //     />
      //   );
      default:
        throw new Error("Unknown step");
    }
  }

  // async function submitOrder(data: FieldValues) {
  //   setLoading(true);
  //   // const { nameOnCard, saveAddress, ...address } = data;
  //   // if (!stripe || !elements) return; // stripe not ready
  //   // try {
  //   //   const cardElement = elements.getElement(CardNumberElement); //get card cvc, expiry date, card number into one variable
  //   //   const paymentResult = await stripe.confirmCardPayment(
  //   //     basket?.clientSecret!,
  //   //     {
  //   //       payment_method: {
  //   //         card: cardElement!,
  //   //         billing_details: {
  //   //           name: nameOnCard,
  //   //         },
  //   //       },
  //   //     }
  //   //   );
  //     // console.log(paymentResult);
  //     // if (paymentResult.paymentIntent?.status === "succeeded") {
  //       const orderNumber = await agent.Orders.create({
  //         saveAddress,
  //         shippingAddress: address,
  //       });
  //       setOrderNumber(orderNumber);
  //       // setPaymentSucceeded(true);
  //       // setPaymentMessage("Thank you - we have received your payment");
  //       setActiveStep(activeStep + 1);
  //       dispatch(clearBasket());
  //       setLoading(false);
  //     } else {
  //       // setPaymentMessage(paymentResult.error?.message!);
  //       // setPaymentSucceeded(false);
  //       setLoading(false);
  //       setActiveStep(activeStep + 1);
  //     }
  //   } catch (error) {
  //     console.log(error);
  //     setLoading(false);
  //   }
  // }

  async function submitOrder(data: FieldValues) {
    setLoading(true);
    try {
      //sleep for 1 seconds
      // await new Promise((resolve) => setTimeout(resolve, 1000));
      await agent.Basket.checkout(data);
      setActiveStep(activeStep + 1);
      dispatch(clearBasket());
      setTimeout(() => {
        dispatch(resetProductsLoaded());
        console.log('Sent');
      }, 1000);
      console.log("Order submitted", data);
      toast.success("Order submitted successfully");
      setPaymentMessage("Thank you! We have received your request.");
      setPaymentSucceeded(true);
      setLoading(false);
    }
    catch (error: any) {
      console.log(error);
      toast.error(`${error.data.message ?? "Problem submitting order"}`);
      setPaymentMessage(`${error.data.message ?? "Problem submitting order"}`);
      setPaymentSucceeded(false);
      setLoading(false);
    }
  }

  const handleNext = async (data: FieldValues) => {
    if (activeStep === steps.length - 1) {
      await submitOrder(data);
    } else {
      setActiveStep(activeStep + 1);
    }
  };

  const handleBack = () => {
    setActiveStep(activeStep - 1);
  };

  const submitDisabled = () => {
    if (activeStep === steps.length - 1) {
      return (
        // !cardComplete.cardCvc ||
        // !cardComplete.cardExpiry ||
        // !cardComplete.cardNumber ||
        !methods.formState.isValid
      );
    } else {
      return !methods.formState.isValid;
    }
  };

  return (
    <FormProvider {...methods}>
      <Paper
        variant="outlined"
        sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}
      >
        <Typography component="h1" variant="h4" align="center">
          Checkout
        </Typography>
        <Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}>
          {steps.map((label) => (
            <Step key={label}>
              <StepLabel>{label}</StepLabel>
            </Step>
          ))}
        </Stepper>
        <>
          {activeStep === steps.length ? (
            <>
              <Typography variant="h5" gutterBottom>
                {paymentMessage}
              </Typography>
              {paymentSucceeded ? (
                <Typography variant="subtitle1">
                  Success. Your order number is being processed.
                </Typography>
              ) : (
                <Button variant="contained" onClick={handleBack}>
                  Go back and try again
                </Button>
              )}
            </>
          ) : (
            <form onSubmit={methods.handleSubmit(handleNext)}>
              {getStepContent(activeStep)}
              <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
                {activeStep !== 0 && (
                  <Button onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
                    Back
                  </Button>
                )}
                <LoadingButton
                  variant="contained"
                  loading={loading}
                  type="submit"
                  sx={{ mt: 3, ml: 1 }}
                  disabled={submitDisabled()}
                >
                  {activeStep === steps.length - 1 ? "Place order" : "Next"}
                </LoadingButton>
              </Box>
            </form>
          )}
        </>
      </Paper>
    </FormProvider>
  );
}
